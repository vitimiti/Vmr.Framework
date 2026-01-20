# Vmr.Framework

🎮 A multipurpose, open source, cross‑platform framework for game development in modern .NET.

## 🧭 Roadmap

This section tracks planned and in‑progress features.

> Note: Apple platforms are not planned due to tooling constraints.

### 🪟 Window Management — `0%` - `0%`
![🪟 Window Management — `0%`](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Fwindow-management-0.json)
`[░░░░░░░░░░] 0%`

![Window Management](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/window-management.json)
`[░░░░░░░░░░] 0%`
- [ ] 🧩 Open and manage a window with a **hardware renderer** on **Win32**
- [ ] 🧩 Open and manage a window with a **hardware renderer** on **X11**
- [ ] 🧩 Open and manage a window with a **hardware renderer** on **Wayland**

### 🎨 Hardware Renderer — `0%` - `0%`
![🎨 Hardware Renderer — `0%`](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Fhardware-renderer-0.json)
`[░░░░░░░░░░] 0%`

![Hardware Renderer](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/hardware-renderer.json)
`[░░░░░░░░░░] 0%`
- [ ] ⚡ **DirectX** support
- [ ] 🧪 **OpenGL Core** support
- [ ] 🔥 **Vulkan** support

### 🧰 Utilities — `0%` - `0%`
![🧰 Utilities — `0%`](https://img.shields.io/endpoint?url=https%3A%2F%2Fraw.githubusercontent.com%2Fvitimiti%2FVmr.Framework%2Fmain%2Fbadges%2Futilities-0.json)
`[░░░░░░░░░░] 0%`

![Utilities](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/utilities.json)
`[░░░░░░░░░░] 0%`

#### ⏱️ Game Time — `0%`

![Game Time](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/game-time.json)
`[░░░░░░░░░░] 0%`
- [ ] Delta time (frame time)
- [ ] Time since initialization
- [ ] Fixed time step (for deterministic updates)
- [ ] Time scaling (pause, slow‑mo, fast‑forward)
- [ ] Frame counter & FPS metrics

#### 🗂️ File System — `0%`

![File System](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/file-system.json)
`[░░░░░░░░░░] 0%`
- [ ] Pack format with optional compression (default + custom codecs)
- [ ] Custom pack format + extension support (with sensible defaults)
- [ ] Virtual file system (disk + packs, transparent access)
- [ ] Mount priorities (disk override pack or vice‑versa)
- [ ] Async file read APIs for large assets
- [ ] Hot‑reload hooks (optional)

#### 🎮 Input System — `0%`

![Input System](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/input-system.json)
`[░░░░░░░░░░] 0%`
- [ ] Keyboard + mouse
- [ ] Gamepad
- [ ] Joystick
- [ ] Action mapping (one action ⇢ many bindings)
- [ ] Axis handling + deadzones
- [ ] Per‑device input states (pressed/held/released)
- [ ] Rebinding at runtime (optional)

#### 🧱 Core Utilities — `0%`

![Core Utilities](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/core-utilities.json)
`[░░░░░░░░░░] 0%`
- [ ] Logging system (levels, sinks)
- [ ] Configuration system (JSON/INI + overrides)
- [ ] Math types (Vec2/3/4, Mat4, Rect, Color)
- [ ] Resource lifetime helpers (ID/handle system)
- [ ] Event / messaging bus (lightweight)

#### 🔊 Audio — `0%`

![Audio](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/audio.json)
`[░░░░░░░░░░] 0%`
- [ ] Sound playback (one‑shot + looping)
- [ ] Streaming audio (music)
- [ ] Volume & mixer groups
- [ ] Basic spatial audio (2D panning)

#### 🧩 2D Renderer Utilities — `0%`

![2D Renderer Utilities](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/2d-renderer-utilities.json)
`[░░░░░░░░░░] 0%`
- [ ] Sprite batching
- [ ] Texture atlas support
- [ ] Simple 2D primitives (rect/line/circle)

#### 🛠️ Content Pipeline — `0%`

![Content Pipeline](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/content-pipeline.json)
`[░░░░░░░░░░] 0%`
- [ ] Asset import/conversion into packs
- [ ] Build caching & incremental builds
- [ ] CLI tooling

#### 🌳 Scene System — `0%`

![Scene System](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/scene-system.json)
`[░░░░░░░░░░] 0%`
- [ ] Scene graph (nodes, hierarchy)
- [ ] Transform system (2D)
- [ ] Component model (basic)

#### 🧬 ECS (Later) — `0%`

![ECS (Later)](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/<OWNER>/<REPO>/main/badges/ecs-later.json)
`[░░░░░░░░░░] 0%`
- [ ] Entity/component data model
- [ ] Systems + scheduling
- [ ] Optional compatibility layer with Scene system
