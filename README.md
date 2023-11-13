# Skylines Auxiliary Methods (SAM)

SAM is a comprehensive class library designed to streamline and enhance the modding experience for Cities Skylines 2. This toolkit offers a range of helper methods and utilities, making it easier for modders to create, customize, and extend their mods with efficiency and ease.

## Features

- **Easy-to-Use API**: Simplified functions and classes that abstract away complex coding requirements.
- **Enhanced Modding Capabilities**: Tools designed specifically for Cities Skylines 2 to enhance gameplay, visuals, and mechanics.
- **Performance Optimization**: Methods optimized for minimal performance impact.
- **Regular Updates**: Continuously updated to stay compatible with the latest game patches and expansions.

## Installation

1. Download the latest release from the [Releases](link-to-releases-page) page. (Not currently available)
2. Extract the files into your Cities Skylines 2 modding folder.
3. Reference SAM in your mod project.
4. Start using the methods in your mod development.

## Usage

```csharp
// Example usage of SAM
using UnityEngine;
using SAM;

// Executed in some game system
raycastHelper = new RaycastHelper( World.GetOrCreateSystemManaged<ToolRaycastSystem>( ), EntityManager );

// Run a raycast
if ( raycastHelper.Raycast( transform.position, out var hit ) )
	Debug.Log( "Raycast success: " + hit.m_Hit.m_Position );	
```

## Note

For now there is just a select few classes to use, this will grow as time progresses.