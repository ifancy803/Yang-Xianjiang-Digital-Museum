# 杨贤江数字纪念馆

## 项目简介

**杨贤江数字纪念馆** 是一个基于 Unity 引擎开发的沉浸式 3D 数字纪念馆项目。项目以著名教育家杨贤江先生为主题，通过三维虚拟博物馆的形式，打造了一个可交互、可漫游的数字纪念空间。

馆内分为多个主题展区，陈列了丰富的图文资料、先生雕像、语录等，结合背景音乐、灯光烘托和交互控制，为用户提供沉浸式的参观体验。该项目适用于教育展览、文化纪念、数字博物馆等场景。

## 技术栈

| 类别                  | 技术                                                  |
| --------------------- | ----------------------------------------------------- |
| **引擎**        | Unity 2022.3.57f1c2 (LTS)                             |
| **渲染管线**    | Built-in Render Pipeline（默认管线）                  |
| **开发语言**    | C#                                                    |
| **UI 系统**     | Unity UI (uGUI) + TextMeshPro 3.0.7                   |
| **后处理**      | Unity Post Processing 3.4.0                           |
| **动画工具**    | Unity Timeline 1.7.6                                  |
| **可视化脚本**  | Visual Scripting 1.9.4                                |
| **3D 模型导入** | UniGLTF (glTF/GLB 格式支持)                           |
| **光照系统**    | 烘焙光照贴图 (Lightmap) + 反射探针 (Reflection Probe) |
| **资源包**      | AK Studio Art - Art Gallery Museum VR                 |

## 技术亮点

### 1. Built-in 渲染管线

基于 Unity 默认 Built-in Render Pipeline，兼容性广泛，配合自定义后处理配置实现博物馆场景的视觉呈现。

### 2. 高品质烘焙光照

使用预烘焙光照贴图（Lightmap）和反射探针系统，实现逼真的博物馆光照效果，营造沉浸式的展厅氛围。

### 3. 第一人称漫游控制器

自定义 [MuseumCameraController](file:///f:/UNITY/UNITYPROJECT/Museum/Assets/Scripts/Camera/MuseumCameraController.cs) 组件，实现了：

- WASD 键位移动 + 鼠标视角控制
- Shift 加速奔跑
- 空格跳跃 + 重力模拟
- CharacterController 物理碰撞

### 4. 自由飞镜头模式

[FreeFlyCamera](file:///f:/UNITY/UNITYPROJECT/Museum/Assets/Scripts/Camera/FreeFlyCamera.cs) 组件提供右键激活的自由飞行视角，支持：

- Q/E 上下升降
- Shift 快速飞行
- 上下视角限位防翻转

### 5. 定时淡入淡出动效

[TimedVisibilityWithFade](file:///f:/UNITY/UNITYPROJECT/Museum/Assets/Scripts/TimedVisibilityWithFade.cs) 组件实现了基于 CanvasGroup 的 UI 定时显隐系统，支持延迟显示、停留时长、淡入淡出时长等参数配置。

### 6. 多展区布局

展馆划分为 6 个独立展区（Assets/Resources/1~6），每个展区可独立配置展品、图文材质和布局，便于内容扩展和管理。

### 7. 中文字体渲染

使用 TextMeshPro SDF（Signed Distance Field）技术渲染中文字体，保证文字清晰锐利，支持仿宋_GB2312 等中文字体。

### 8. glTF/GLB 模型支持

集成 UniGLTF 工具链，支持 glTF/GLB 标准 3D 模型格式的导入和导出，便于外部 3D 资源的接入。

## 工程文件结构

```
Museum/
├── Assets/
│   ├── AK Studio Art/Art Gallery Museum VR/   # 博物馆 3D 美术资源包
│   │   ├── Materials/                         # 材质球（展品、建筑、灯光等）
│   │   ├── Models/                            # 3D 模型（FBX 格式）
│   │   ├── Prefabs/                           # 预制体
│   │   ├── Textures/                          # 纹理贴图
│   │   └── Scenes/                            # 示例场景（Demo.unity）
│   │
│   ├── Scripts/                               # 自定义 C# 脚本
│   │   ├── Camera/
│   │   │   ├── MuseumCameraController.cs      # 第一人称漫游控制器
│   │   │   └── FreeFlyCamera.cs               # 自由飞行相机
│   │   └── TimedVisibilityWithFade.cs         # 定时淡入淡出
│   │
│   ├── Scenes/                                # 场景文件
│   │   ├── Demo2/                             # 主场景烘焙光照数据
│   │   ├── Demo2.unity                        # 主场景
│   │   └── SampleScene.unity                  # 默认示例场景
│   │
│   ├── Resources/                             # 运行时资源
│   │   ├── 1/ ~ 6/                            # 六个展区资源
│   │   ├── Audio/                             # 背景音乐
│   │   ├── Fonts/                             # 字体文件（仿宋_GB2312）
│   │   ├── Model/                             # 运行时模型
│   │   ├── PostPocessing/                     # 后处理配置文件
│   │   └── else/                              # 其他图片资源
│   │
│   ├── Render/                                # 渲染管线配置文件
│   ├── TextMesh Pro/                          # TextMeshPro 字体和着色器
│   └── UniGLTF/                               # glTF/GLB 导入工具
│
├── ProjectSettings/                           # Unity 项目设置
│   ├── ProjectSettings.asset                  # 通用项目设置
│   ├── ProjectVersion.txt                     # Unity 版本信息
│   ├── GraphicsSettings.asset                 # 图形设置
│   ├── QualitySettings.asset                  # 质量设置
│   ├── InputManager.asset                     # 输入管理器
│   ├── XRSettings.asset                       # XR 设置
│   └── ...
│
├── Packages/                                  # 包管理
│   └── manifest.json                          # 依赖包清单
│
├── .gitignore                                 # Git 忽略规则
└── README.md                                  # 项目说明文档
```

## 场景说明

| 场景              | 说明                                                                     |
| ----------------- | ------------------------------------------------------------------------ |
| Demo2.unity       | **主场景**，包含完整的博物馆布局、烘焙光照、反射探针和所有展区内容 |
| SampleScene.unity | Unity 默认示例场景                                                       |

## 快速开始

1. 使用 **Unity 2022.3.57f1c2** 或更高版本打开项目
2. 在 Project 窗口导航至 `Assets/Scenes/Demo2.unity` 打开主场景
3. 点击 Play 进入运行模式
4. **WASD** 移动 · **鼠标** 旋转视角 · **Shift** 加速 · **空格** 跳跃
5. **鼠标右键** 拖拽可激活自由飞行视角（**Q/E** 上下升降）

## 操作说明

| 操作              | 功能             |
| ----------------- | ---------------- |
| W / A / S / D     | 前后左右移动     |
| 鼠标移动          | 旋转视角         |
| Shift             | 加速移动         |
| 空格              | 跳跃             |
| 鼠标右键（按住）  | 切换自由飞行模式 |
| Q / E（飞行模式） | 下降 / 上升      |
