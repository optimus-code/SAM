using Game;
using Game.Common;
using Game.Net;
using Game.Tools;
using Unity.Entities;
using Unity.Mathematics;

namespace SAM.Systems
{
    /// <summary>
    /// A helper class for simplifying raycasting in the game.
    /// This class encapsulates the raycasting logic, making it easier to use within the game.
    /// </summary>
    public class SimpleRaycastSystem : GameSystemBase
    {
        private ToolRaycastSystem _toolRaycastSystem;

        protected override void OnCreate( )
        {
            base.OnCreate( );

            _toolRaycastSystem = World.GetExistingSystemManaged<ToolRaycastSystem>( );
        }

        protected override void OnUpdate( )
        {
            // Not used
        }

        /// <summary>
        /// Sets up the raycasting parameters for the raycast system.
        /// </summary>
        /// <param name="origin">The origin point of the raycast.</param>
        /// <param name="flags">The raycast flags that define the behavior of the raycast.</param>
        /// <param name="typeMask">The type mask that determines the types of objects to be raycast against.</param>
        /// <param name="collisionMask">The collision mask that defines the collision layers for the raycast.</param>
        private void SetupRaycast( UnityEngine.Vector3 origin, RaycastFlags flags, TypeMask typeMask, CollisionMask collisionMask )
        {
            _toolRaycastSystem.raycastFlags = flags;
            _toolRaycastSystem.typeMask = typeMask;
            _toolRaycastSystem.collisionMask = collisionMask;
            _toolRaycastSystem.rayOffset = ( float3 ) origin;
        }

        /// <summary>
        /// Performs a raycast using the specified parameters.
        /// </summary>
        /// <param name="origin">The origin point of the raycast.</param>
        /// <param name="hitInfo">The result of the raycast, including the hit entity and collision information.</param>
        /// <param name="flags">Optional raycast flags to customize the raycast behavior.</param>
        /// <param name="typeMask">Optional type mask to filter the types of objects raycasted against.</param>
        /// <param name="collisionMask">Optional collision mask to filter the collision layers for the raycast.</param>
        /// <returns>True if the raycast hits an entity, false otherwise.</returns>
        public bool Raycast( UnityEngine.Vector3 origin, out RaycastResult hitInfo, RaycastFlags flags = ~( RaycastFlags.ElevateOffset | RaycastFlags.SubElements | RaycastFlags.Placeholders | RaycastFlags.Markers | RaycastFlags.NoMainElements | RaycastFlags.UpgradeIsMain | RaycastFlags.OutsideConnections | RaycastFlags.Outside | RaycastFlags.Cargo | RaycastFlags.Passenger | RaycastFlags.Decals | RaycastFlags.EditorContainers ), TypeMask typeMask = TypeMask.All, CollisionMask collisionMask = CollisionMask.OnGround | CollisionMask.Overground )
        {
            SetupRaycast( origin, flags, typeMask, collisionMask );

            return _toolRaycastSystem.GetRaycastResult( out hitInfo ) && !EntityManager.HasComponent<Deleted>( hitInfo.m_Owner );
        }

        /// <summary>
        /// Performs a raycast using the specified parameters.
        /// </summary>
        /// <param name="origin">The origin point of the raycast.</param>
        /// <param name="controlPoint">The result of the raycast, represents a control point of a Network.</param>
        /// <param name="flags">Optional raycast flags to customize the raycast behavior.</param>
        /// <param name="typeMask">Optional type mask to filter the types of objects raycasted against.</param>
        /// <param name="collisionMask">Optional collision mask to filter the collision layers for the raycast.</param>
        /// <returns>True if the raycast hits an entity, false otherwise.</returns>
        public bool Raycast( UnityEngine.Vector3 origin, out ControlPoint controlPoint, RaycastFlags flags = ~( RaycastFlags.ElevateOffset | RaycastFlags.SubElements | RaycastFlags.Placeholders | RaycastFlags.Markers | RaycastFlags.NoMainElements | RaycastFlags.UpgradeIsMain | RaycastFlags.OutsideConnections | RaycastFlags.Outside | RaycastFlags.Cargo | RaycastFlags.Passenger | RaycastFlags.Decals | RaycastFlags.EditorContainers ), TypeMask typeMask = TypeMask.All, CollisionMask collisionMask = CollisionMask.OnGround | CollisionMask.Overground )
        {
            if ( Raycast( origin, out RaycastResult hitInfo, flags, typeMask, collisionMask ) )
            {
                var entity = hitInfo.m_Owner;

                if ( EntityManager.HasComponent<Node>( entity ) && EntityManager.HasComponent<Edge>( hitInfo.m_Hit.m_HitEntity ) )
                    entity = hitInfo.m_Hit.m_HitEntity;

                controlPoint = new ControlPoint( entity, hitInfo.m_Hit );
                return true;
            }

            controlPoint = default;
            return false;
        }
    }
}
