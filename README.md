# Vmr.Framework

A multipurpose, open source, cross‑platform framework for game development in modern .NET.

## Roadmap

This section tracks planned and in‑progress features.

> Note: Apple platforms are not planned due to tooling constraints.

### Window Management

- [ ] Open and manage a window with a **hardware renderer** on **Win32**
- [ ] Open and manage a window with a **hardware renderer** on **X11**
- [ ] Open and manage a window with a **hardware renderer** on **Wayland**

### Hardware Renderer

- [ ] **DirectX** support
- [ ] **OpenGL Core** support
- [ ] **Vulkan** support

### Utilities

#### Game Time

- [ ] Delta time (frame time)
- [ ] Time since initialization
- [ ] Fixed time step (for deterministic updates)
- [ ] Time scaling (pause, slow‑mo, fast‑forward)
- [ ] Frame counter & FPS metrics

#### File System

- [ ] Pack format with optional compression (default + custom codecs)
- [ ] Custom pack format + extension support (with sensible defaults)
- [ ] Virtual file system (disk + packs, transparent access)
- [ ] Mount priorities (disk override pack or vice‑versa)
- [ ] Async file read APIs for large assets
- [ ] Hot‑reload hooks (optional)

#### Input System

- [ ] Keyboard + mouse
- [ ] Gamepad
- [ ] Joystick
- [ ] Action mapping (one action ⇢ many bindings)
- [ ] Axis handling + deadzones
- [ ] Per‑device input states (pressed/held/released)
- [ ] Rebinding at runtime (optional)

#### Core Utilities

- [ ] Logging system (levels, sinks)
- [ ] Configuration system (JSON/INI + overrides)
- [ ] Math types (Vec2/3/4, Mat4, Rect, Color)
- [ ] Resource lifetime helpers (ID/handle system)
- [ ] Event / messaging bus (lightweight)

#### Audio

- [ ] Sound playback (one‑shot + looping)
- [ ] Streaming audio (music)
- [ ] Volume & mixer groups
- [ ] Basic spatial audio (2D panning)

#### 2D Renderer Utilities

- [ ] Sprite batching
- [ ] Texture atlas support
- [ ] Simple 2D primitives (rect/line/circle)

#### Content Pipeline

- [ ] Asset import/conversion into packs
- [ ] Build caching & incremental builds
- [ ] CLI tooling

#### Scene System

- [ ] Scene graph (nodes, hierarchy)
- [ ] Transform system (2D)
- [ ] Component model (basic)