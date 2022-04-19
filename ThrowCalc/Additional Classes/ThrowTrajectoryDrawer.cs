using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThrowCalc
{
    // Mainly adapted from: https://stackoverflow.com/a/37694059
    class ThrowTrajectoryDrawer : MonoBehaviour
    {
        public Fighting FightingInstance;

        private Vector3 _gravity;
        private Vector3 _playerViewingAngle; // This is a character's viewing direction
        private LineRenderer _lineRenderer;

        private Transform _aimPosMain;
        private Transform _aimPosHelper;

        private List<Vector3> _plotPoints = new (250);

        void Start()
        {
            _gravity = Physics.gravity;

            _lineRenderer = gameObject.AddComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.positionCount = _plotPoints.Capacity;

            _aimPosMain = GetComponentInChildren<AimTarget>().transform;
            _aimPosHelper = GetComponentInChildren<AimTargetHelper>().transform;

            _lineRenderer.material = MatchmakingHandler.Instance.IsInsideLobby
                ? MultiplayerManagerAssets.Instance.Colors[FightingInstance.GetComponentInParent<Controller>().playerID]
                : MultiplayerManagerAssets.Instance.Colors[0];
        }

        void Update()
        {
            _playerViewingAngle = _aimPosHelper.forward;

            if (FightingInstance.weapon)
            {
                if (FightingInstance.weapon.gradualRotationSpeed > 1000f) _playerViewingAngle = _aimPosMain.forward;

                Vector3 velocity = _playerViewingAngle * 35f;
                Vector3 startPos = new Vector3(0f, _aimPosMain.position.y, _aimPosMain.position.z) - _aimPosMain.forward * 0.5f;

                _plotPoints.Clear();

                for (int i = 0; i < _plotPoints.Capacity; i++)
                {
                    velocity += _gravity * Time.fixedDeltaTime;
                    velocity *= Mathf.Clamp01(1f - 0.3f * Time.fixedDeltaTime); // Accounting for drag of the weapon (always 0.3 for all guns) to the velocity via a simple percentage reduction
                    startPos += velocity * Time.fixedDeltaTime;
                    _plotPoints.Add(startPos);
                }

                _lineRenderer.SetPositions(_plotPoints.ToArray()); // Render the trajectory line. Will form the shape of parabola
            }
            
        }
    }
}
