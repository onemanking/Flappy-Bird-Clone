# Flappy Bird Clone - Technical Summary

## Architecture

The project follows a well-structured object-oriented architecture with the following key design patterns:

### Design Patterns

1. **Singleton Pattern**
   - Used for manager classes like `GameManager`, `PoolManager`, `CameraController`
   - For easy access to shared resources and game state management

2. **Object Pooling**
   - Implemented via `PoolManager` class
   - Handles recycling of frequently spawned objects
   - Reduces memory allocations and garbage collection overhead

3. **Event-Driven Communication**
   - Uses `EventBus` for decoupled communication between systems
   - Implements a publisher-subscriber pattern for game events

4. **Component-Based Design**
   - Follows Unity's component system architecture
   - Base classes such as `BaseObject` and `BasePlayerObject` provide shared functionality

5. **Configuration System**
   - Data-driven approach with config classes in the `Datas/` folder
   - Separates game data from behavior logic
   - Provides ScriptableObject-based configurations for all major game components

## Core Systems

### Game State Management

- `GameManager` handles the overall game state (`Idle`, `Starting`, `GameOver`)
- State transitions are communicated via the event system

### Player Control

- `Bird` class extends `BasePlayerObject` for the player-controlled character
- `PlayerController` handles input detection and triggers appropriate actions
- Physics-based movement with velocity and rotation updates

### Obstacle System

- `PipeManager` controls the spawning and positioning of pipe obstacles
- `PipeContainer` serves as the container for top and bottom pipe pairs
- Dynamic difficulty adjustment through pipe spacing configuration via `PipeContainerConfig`

### Pooling System

- Custom pooling implementation using Unity's `ObjectPool<T>`
- `IPool` interface ensures all pooled objects implement required lifecycle methods
- Optimizes performance by reusing objects

### Camera System

- `CameraController` manages the game camera and follows the player character
- Configurable camera behavior via `CameraConfig`

## Technical Features

### Configuration System

- Extensive use of ScriptableObject-based configuration files:
  - `BasePlayerObjectConfig` - Base configuration for player-controlled objects
  - `BirdConfig` - Bird-specific parameters including jump force and rotation settings
  - `CameraConfig` - Camera follow behavior and smoothing settings
  - `PipeContainerConfig` - Pipe appearance and behavior configuration
  - `PipeSpawnConfig` - Controls frequency, height variation, and gap size of pipe spawning
  - `PoolingConfig` - Object pool size and recycling behavior settings
- Separation of data from logic allows for:
  - Easy balancing and tuning without code changes
  - Designer-friendly adjustment of gameplay parameters
  - Runtime configuration switching for different difficulty levels
- Constants class for game-wide shared values

### Object Lifecycle Management

- Clear separation between initialization, spawning, and despawning logic
- Controlled object activation/deactivation

### Event System

- `EventBus` provides a centralized event management system
- Events for game state changes, player actions, and scoring

### Parallax Effects

- `GroundParallax` creates the ground movement for parallax scrolling

### UI System

- Separate UI classes for different game screens (`GameUI`, `MainMenuUI`)
- UI interaction connected to game logic via events

## Development Approach

The code demonstrates several best practices in game development:

1. **Separation of Concerns** - Logic is divided into specialized classes with clear responsibilities
2. **Scalability** - Architecture allows for easy expansion of features
3. **Performance Optimization** - Object pooling reduces garbage collection overhead
4. **Data-Driven Design** - Game parameters are externalized in config files
5. **Maintainability** - Clean code organization with well-named methods and classes

### Configuration Architecture

The project makes extensive use of ScriptableObject-based configurations to separate data from logic:

1. **Player Configuration**
   - `BasePlayerObjectConfig` defines base movement and physics properties
   - `BirdConfig` extends with bird-specific parameters like jump force, rotation limits, and visual effects

2. **Environment Configuration**
   - `CameraConfig` controls camera follow behavior, smoothing, and boundary constraints
   - `PipeContainerConfig` manages visual appearance and collision properties of pipe obstacles
   - `PipeSpawnConfig` handles spacing, height variation, and gap sizes between pipes

3. **System Configuration**
   - `PoolingConfig` defines object pooling settings for performance optimization
   - `Constants` provides game-wide reference values and thresholds

This configuration-driven approach enables:

- Quick iteration and fine-tuning of gameplay without code changes
- Easy implementation of difficulty levels by swapping config assets
- Clear separation between design parameters and programming logic
- Improved workflow for designers who can modify game feel without coding

### Movement Design

A key design decision in this implementation is that only three elements update their positions in the game world:

1. **Player Object (Bird)** - The only game entity that has true physics-based movement
2. **Ground Parallax** - Updates position to create the illusion of forward movement
3. **Camera** - Follows the player object to maintain proper framing

This approach creates the illusion of the player moving through the world while actually keeping obstacles (pipes) stationary, which simplifies collision detection and improves performance. This design choice reflects a common technique in side-scrolling games where the perception of movement is created without moving all game elements.
