# Vmr.Framework

🎮 A multipurpose, open source, cross‑platform framework for game development in modern .NET.

## 🧭 Roadmap

This section tracks planned and in‑progress features.

> Note: Apple platforms are not planned due to tooling constraints.

### 🪟 Window Management — `0%`
![🪟 Window Management](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Fwindow-management.json)
`[░░░░░░░░░░] 0%`
- [ ] 🧩 Open and manage a window with a **hardware renderer** on **Win32**
- [ ] 🧩 Open and manage a window with a **hardware renderer** on **X11**
- [ ] 🧩 Open and manage a window with a **hardware renderer** on **Wayland**

### 🎨 Hardware Renderer — `0%`
![🎨 Hardware Renderer](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Fhardware-renderer.json)
`[░░░░░░░░░░] 0%`
- [ ] ⚡ **DirectX** support
- [ ] 🧪 **OpenGL Core** support
- [ ] 🔥 **Vulkan** support

### 🧰 Utilities — `0%`
![🧰 Utilities](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Futilities.json)
`[░░░░░░░░░░] 0%`

#### ⏱️ Game Time
![⏱️ Game Time](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Fgame-time.json)
`[░░░░░░░░░░] 0%`
- [ ] Delta time (frame time)
- [ ] Time since initialization
- [ ] Fixed time step (for deterministic updates)
- [ ] Time scaling (pause, slow‑mo, fast‑forward)
- [ ] Frame counter & FPS metrics

#### 🗂️ File System
![🗂️ File System](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Ffile-system.json)
`[░░░░░░░░░░] 0%`
- [ ] Pack format with optional compression (default + custom codecs)
- [ ] Custom pack format + extension support (with sensible defaults)
- [ ] Virtual file system (disk + packs, transparent access)
- [ ] Mount priorities (disk override pack or vice‑versa)
- [ ] Async file read APIs for large assets
- [ ] Hot‑reload hooks (optional)

#### 🎮 Input System
![🎮 Input System](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Finput-system.json)
`[░░░░░░░░░░] 0%`
- [ ] Keyboard + mouse
- [ ] Gamepad
- [ ] Joystick
- [ ] Action mapping (one action ⇢ many bindings)
- [ ] Axis handling + deadzones
- [ ] Per‑device input states (pressed/held/released)
- [ ] Rebinding at runtime (optional)

#### 🧱 Core Utilities
![🧱 Core Utilities](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Fcore-utilities.json)
`[░░░░░░░░░░] 0%`
- [ ] Logging system (levels, sinks)
- [ ] Configuration system (JSON/INI + overrides)
- [ ] Math types (Vec2/3/4, Mat4, Rect, Color)
- [ ] Resource lifetime helpers (ID/handle system)
- [ ] Event / messaging bus (lightweight)

#### 🔊 Audio
![🔊 Audio](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Faudio.json)
`[░░░░░░░░░░] 0%`
- [ ] Sound playback (one‑shot + looping)
- [ ] Streaming audio (music)
- [ ] Volume & mixer groups
- [ ] Basic spatial audio (2D panning)

#### 🧩 2D Renderer Utilities
![🧩 2D Renderer Utilities](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2F2d-renderer-utilities.json)
`[░░░░░░░░░░] 0%`
- [ ] Sprite batching
- [ ] Texture atlas support
- [ ] Simple 2D primitives (rect/line/circle)

#### 🛠️ Content Pipeline
![🛠️ Content Pipeline](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Fcontent-pipeline.json)
`[░░░░░░░░░░] 0%`
- [ ] Asset import/conversion into packs
- [ ] Build caching & incremental builds
- [ ] CLI tooling

#### 🌳 Scene System
![🌳 Scene System](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Fscene-system.json)
`[░░░░░░░░░░] 0%`
- [ ] Scene graph (nodes, hierarchy)
- [ ] Transform system (2D)
- [ ] Component model (basic)

#### 🧬 ECS (Later)
![🧬 ECS (Later)](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Fecs-later.json)
`[░░░░░░░░░░] 0%`
- [ ] Entity/component data model
- [ ] Systems + scheduling
- [ ] Optional compatibility layer with Scene system
