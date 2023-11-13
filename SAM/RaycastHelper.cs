using Game.Common;
using Game.Tools;
using Unity.Entities;
using Unity.Mathematics;

namespace SAM
{
    /// <summary>
    /// A helper class for simplifying raycasting in the game.
    /// This class encapsulates the raycasting logic, making it easier to use within the game.
    /// </summary>
    public class RaycastHelper
    {
        private ToolRaycastSystem m_ToolRaycastSystem;
        private EntityManager m_EntityManager;

        /// <summary>
        /// Initializes a new instance of the RaycastHelper class.
        /// </summary>
        /// <param name="toolRaycastSystem">The raycasting system used in the game.</param>
        /// <param name="entityManager">The entity manager responsible for managing game entities.</param>
        public RaycastHelper( ToolRaycastSystem toolRaycastSystem, EntityManager entityManager )
        {
            m_ToolRaycastSystem = toolRaycastSystem;
            m_EntityManager = entityManager;
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
            m_ToolRaycastSystem.raycastFlags = flags;
            m_ToolRaycastSystem.typeMask = typeMask;
            m_ToolRaycastSystem.collisionMask = collisionMask;
            m_ToolRaycastSystem.rayOffset = ( float3 ) origin;
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

            return m_ToolRaycastSystem.GetRaycastResult( out hitInfo ) && !m_EntityManager.HasComponent<Deleted>( hitInfo.m_Owner );
        }
    }
}
