# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Sushi Idle Game** - An idle restaurant management game built with Unity 2022.3.20f1. Players manage a sushi restaurant by hiring employees, serving customers, and generating revenue. The game uses modern Unity development practices with Zenject for dependency injection, UniTask for async operations, and DOTween for animations.

## Unity Project Structure

The actual Unity project is located at:
```
Sushi-Restraunt-Game/Sushi bar Game/
```

Always navigate to this directory before running Unity commands:
```bash
cd "Desktop/Unity Projects/Sushi Idle Game/Sushi-Restraunt-Game/Sushi bar Game"
```

## Key Technologies

- **Unity Version**: 2022.3.20f1
- **Zenject**: Dependency injection framework (in `Assets/Plugins/Zenject/`)
- **UniTask**: Async/await for Unity (in `Assets/Plugins/UniTask/`)
- **DOTween**: Animation tweening library (in `Assets/Plugins/Demigiant/DOTween/`)
- **Odin Inspector**: Inspector enhancement tool (in `Assets/Plugins/Sirenix/`)
- **TextMesh Pro**: Text rendering
- **AI Navigation**: NavMesh for character pathfinding

## Code Architecture

The project follows a modular architecture with clear separation between Core systems and Content-specific implementations.

### Module Structure

```
Assets/Modules/
├── Core/                          # Framework and reusable systems
│   ├── Data/                      # Data structures and ScriptableObjects
│   ├── Extensions/                # Extension methods and utilities
│   ├── Factories/                 # Factory pattern implementations
│   ├── Managers/                  # High-level game systems
│   ├── Pools/                     # Object pooling
│   ├── Serializeable Collections/ # Custom serializable collections
│   ├── Services/                  # Business logic services
│   └── Zenject/                   # DI installers (CRITICAL)
└── Content/                       # Game-specific implementations
    ├── Characters/                # Customer and Employer systems
    ├── Food Generator/            # Food creation logic
    ├── FoodCollection/            # Food types and data
    ├── Item/                      # Shop items
    ├── Map Points/                # Waypoint system for navigation
    ├── Player Resources/          # Currency and resources
    ├── Shop/                      # Shop UI and logic
    └── UI/                        # UI components and buttons
```

### Zenject Dependency Injection

**IMPORTANT**: This project heavily uses Zenject for dependency injection. All major systems are registered through installers located in `Assets/Modules/Core/Zenject/`.

#### Key Installers (Read these first when modifying systems):

- **InstallerServices.cs** - Registers all service layer classes:
  - `ServiceMapPoint` - Map point management
  - `ServiceCustomerQueue` - Customer queueing system
  - `ServiceFoodGenerators` - Food generation logic

- **InstallerManagers.cs** - Registers manager classes:
  - `ManagerShop` - Shop system with items and purchases
  - `ManagerEmployer` - Employee spawning and management
  - `ManagerCustomer` - Customer spawning and lifecycle
  - `ManagerScreen` - UI screen management

- **InstallerFactories.cs** - Registers factory classes for object creation
- **InstallerModels.cs** - Registers model/data classes
- **InstallerViewModels.cs** - Registers view-model classes
- **InstallerSerializeableCollections.cs** - Registers serializable collection instances

#### Zenject Best Practices for this Project:

1. **Always check installers first** before modifying core systems
2. **Use constructor injection** - Most classes receive dependencies via `[Inject]` constructor
3. **Register new services** in appropriate installer (Services go in `InstallerServices.cs`, Managers in `InstallerManagers.cs`, etc.)
4. **Scene Context** - Check the Gameplay scene for the SceneContext GameObject that references all installers
5. **Binding lifetimes**:
   - `AsSingle()` - Singleton across scene
   - `NonLazy()` - Force instantiation at startup
   - `WithArguments()` - Pass specific values to constructors

### Character System (MVC Pattern)

Characters (Customer and Employer) follow a Model-View-Controller pattern:

**Structure**:
```
Characters/
├── Base/
│   ├── BaseController.cs      # Base controller logic
│   └── BaseView.cs             # Base view with MonoBehaviour
├── Customer/
│   ├── Controller/             # Customer-specific behavior
│   ├── Model/                  # Customer data (ModelCustomer.cs)
│   ├── View/                   # Customer visuals (ViewCustomer.cs)
│   └── Events/                 # Customer event system
└── Employer/
    ├── Controller/             # Employer-specific behavior
    ├── Model/                  # Employer data (ModelEmployer.cs)
    ├── View/                   # Employer visuals (ViewEmployer.cs, ViewFood.cs)
    └── Events/                 # Employer event system
```

**Key Points**:
- **Model** holds data and business logic (no Unity dependencies)
- **View** handles rendering and Unity-specific behavior (MonoBehaviour)
- **Controller** coordinates between Model and View
- **Events** provide decoupled communication between systems

### Manager Pattern

Managers are high-level systems that coordinate multiple services and control game flow:

- **ManagerScreen** (`ManagerScreen.cs:8-58`) - Manages UI screen transitions, listens to button events, handles screen open/close logic
- **ManagerCustomer** - Controls customer spawning, lifecycle, and queueing
- **ManagerEmployer** - Controls employee hiring, placement, and management
- **ManagerShop** - Controls shop UI, item purchases, and inventory

All managers implement `IInitializable` and `IDisposable` from Zenject for proper lifecycle management.

### Service Pattern

Services contain business logic and coordinate data flow:

- **ServiceMapPoint** - Manages waypoints and pathfinding points for characters
- **ServiceCustomerQueue** - Handles customer queue logic and order processing
- **ServiceFoodGenerators** - Manages food generation stations and timers

### Factory Pattern

Factories handle object creation and are injected via Zenject:

- **FactoryCustomer** - Creates customer instances with proper dependency injection
- **FactoryEmployer** - Creates employer instances with proper dependency injection
- **IFactory** - Base factory interface

### Map Points System

The game uses a waypoint-based navigation system:

- **PointMono** - MonoBehaviour representing a physical point in the scene
- **CollectionPointsMono** - Serializable dictionary of all map points
- Map points use string IDs (e.g., "CSP1" for "Customer Spawn 1")
- See `InstallerManagers.cs:58` for example: `MapPoints["CSP1"]` retrieves spawn point

### Event System

The project uses C# events for decoupled communication:

- **EventsCustomer** - Customer-related events
- **EventsEmployer** - Employer-related events
- **EventsPlayerResources** - Resource change events
- **EventsShop** - Shop interaction events
- **EventsButtonClick** - UI button events (e.g., `OnCloseScreen`, `OnOpenScreen`)

## Common Development Tasks

### Adding a New Service

1. Create service class in `Assets/Modules/Core/Services/`
2. Implement service logic (inject dependencies via constructor)
3. Open `Assets/Modules/Core/Zenject/InstallerServices.cs`
4. Add binding method:
   ```csharp
   private void BindYourService()
   {
       Container
           .BindInterfacesAndSelfTo<YourService>()
           .AsSingle();
   }
   ```
5. Call method in `InstallBindings()`

### Adding a New Manager

1. Create manager class in `Assets/Modules/Core/Managers/`
2. Implement `IInitializable` and `IDisposable` interfaces
3. Add constructor with injected dependencies
4. Open `Assets/Modules/Core/Zenject/InstallerManagers.cs`
5. Add serialized field if manager needs scene references
6. Add binding method similar to existing managers
7. Inject required dependencies with `WithArguments()` if needed

### Adding a New Character Type

1. Create folder structure: `Assets/Modules/Content/Characters/[Type]/`
2. Create Model, View, Controller, and Events classes
3. Inherit from `BaseController` and `BaseView`
4. Create factory in `Assets/Modules/Core/Factories/`
5. Register factory in `InstallerFactories.cs`
6. Add manager if needed for lifecycle control

### Modifying UI Screens

1. UI screens inherit from `BaseScreen`
2. Screen types defined in `ScreenType` enum
3. Screens registered in `CollectionScreens` serializable collection
4. ManagerScreen handles transitions via `EventsButtonClick` events
5. Main scene: `Assets/Scenes/Gameplay.unity`

### Working with Food System

1. **FoodType** enum defines food varieties
2. **FoodGenerator** MonoBehaviour generates food over time
3. **ServiceFoodGenerators** manages all generators
4. Food visuals handled by **ViewFood** component on Employers

### Working with Map Points

1. Find all points in scene via `CollectionPointsMono.MapPoints` dictionary
2. Point IDs are strings (e.g., "CSP1", "BUY1", "KITCHEN1")
3. **PointType** enum categorizes points (Buy, Spawn, Kitchen, etc.)
4. Use `ServiceMapPoint` to query and manage points at runtime

## Building and Testing

### Opening in Unity

```bash
# From project root
cd "Sushi bar Game"

# Windows - Open Unity Hub
start unityhub://[PROJECT_PATH]
```

### Opening in Rider

The project includes a `.sln` file for JetBrains Rider:
```bash
rider "Sushi bar Game.sln"
```

### Running Tests

Unity Test Framework is available. Access via:
- Unity Editor: Window → General → Test Runner
- Or run tests via command line (requires Unity CLI setup)

## Third-Party Assets

### Casual GUI Kit Mobile

Located in `Assets/hardartcore/Casual GUI Kit Mobile/`
- UI sprites, fonts, and prefabs for mobile-friendly interface
- Pre-made button styles and panel designs
- Use these assets for consistent UI look and feel

### Plugin Documentation

- **Zenject**: `Assets/Plugins/Zenject/` - See Zenject documentation at https://github.com/modesttree/Zenject
- **UniTask**: `Assets/Plugins/UniTask/` - See UniTask documentation at https://github.com/Cysharp/UniTask
- **DOTween**: `Assets/Plugins/Demigiant/DOTween/` - See DOTween documentation at http://dotween.demigiant.com/
- **Odin Inspector**: `Assets/Plugins/Sirenix/` - Enhanced inspector and serialization

## Scene Structure

**Main Scene**: `Assets/Scenes/Gameplay.unity`

This scene contains:
- SceneContext (Zenject) with all installers attached
- Camera setup
- UI Canvas with screen collection
- Map points for navigation
- Environment objects

## Important Notes

- **Zenject is critical** - Most systems depend on DI, always check installers before modifications
- **Event-driven architecture** - Systems communicate via events, not direct references
- **MVC for characters** - Keep Model, View, Controller separated for maintainability
- **Map point IDs** - Use meaningful string IDs for waypoints (e.g., "CSP1" = Customer Spawn Point 1)
- **UniTask for async** - Prefer UniTask over Coroutines for async operations
- **Odin Inspector** - Many classes use Odin attributes for inspector enhancement
- **Mobile target** - Consider touch input and performance for mobile platforms
- **NavMesh** - Character movement uses Unity's AI Navigation with NavMeshAgent

## Development Tips

1. **Read installers first** - Understanding the DI setup is crucial for this project
2. **Follow existing patterns** - Study existing Managers, Services, and Factories before creating new ones
3. **Use Events** - Avoid tight coupling by using the event system
4. **Test in Unity Editor** - Always test changes in play mode before building
5. **Check map points** - Verify map point IDs match between code and scene
6. **Profile on device** - Performance matters for mobile idle games
