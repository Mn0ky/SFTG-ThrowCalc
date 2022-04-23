using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThrowCalc
{
    [RequireComponent(typeof(LineRenderer))]
    class ThrowTrajectoryDrawer : MonoBehaviour
    {
        public Fighting FightingInstance { get; set; }

        public int SpawnID { get; private set; }

        private Vector3 _gravity;
        private Vector3 _playerViewingAngle; // This is a character's viewing direction
        private LineRenderer _lineRenderer;

        private Transform _aimPosMain;
        private Transform _aimPosHelper;

        private List<Vector3> _plotPoints = new (250);

        void Start()
        {
            _gravity = Physics.gravity;

            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.positionCount = _plotPoints.Capacity;

            _aimPosMain = GetComponentInChildren<AimTarget>().transform;
            _aimPosHelper = GetComponentInChildren<AimTargetHelper>().transform;

            SpawnID = FightingInstance.GetComponentInParent<Controller>().playerID;

            _lineRenderer.sharedMaterial = MatchmakingHandler.Instance.IsInsideLobby
                ? MultiplayerManagerAssets.Instance.Colors[SpawnID]
                : MultiplayerManagerAssets.Instance.Colors[0];

            enabled = !DisableOtherTrajectories.Toggled;
        }

        // Mainly adapted from: https://stackoverflow.com/a/37694059
        void Update()
        {
            if (FightingInstance.weapon)
            {
                {
                    _playerViewingAngle = _aimPosHelper.forward;
                    if (FightingInstance.weapon.gradualRotationSpeed > 1000f) _playerViewingAngle = _aimPosMain.forward;

                    Vector3 velocity = _playerViewingAngle * 35f;
                    Vector3 startPos = new Vector3(0f, _aimPosMain.position.y, _aimPosMain.position.z) - _aimPosMain.forward * 0.5f;

                    _plotPoints.Clear();

                    for (int i = 0; i < _plotPoints.Capacity; i++)
                    {
                        velocity += _gravity * Time.fixedDeltaTime;
                        // Accounting for drag of the weapon (always 0.3 for all guns) to the velocity via a simple percentage reduction
                        velocity *= Mathf.Clamp01(1f - 0.3f * Time.fixedDeltaTime); 
                        startPos += velocity * Time.fixedDeltaTime;
                        _plotPoints.Add(startPos);
                    }

                    // Render the trajectory line. Will form the shape of parabola
                    _lineRenderer.SetPositions(_plotPoints.ToArray()); 
                }
            }
        }

        void OnDisable() => _lineRenderer.enabled = false;

        void OnEnable() => _lineRenderer.enabled = true;
    }
}
