# Bruise Way Home

## Summary
Game jam project for CGDA Arcade Jam 2026 (GAME 1501).
"Legally distinct Batman" stumbles home drunk. Player navigates obstacles and rescues people being attacked.
Working title: **Bruise Way Home**.

## Team
| Name | GitHub | Role/Notes |
|------|--------|------------|
| Mike (HeadphoneTaco) | HeadphoneTaco / Michael Hardwicke | Project setup, .gitignore, prototype scene |
| Allan | ValleyPenguin | Drunk Sway mechanic, diving, camera, car spawning, skybox |
| Alper | getyourfreak0n / Nejat Alper Satiroglu (NS) | Cinemachine, player rotation, materials, map building |
| Danish | (not yet added — collaborator invite pending) | Has class tomorrow; will be added then |

## Engine
Unity (Git LFS enabled)

## Branches
- master
- origin/Allan
- origin/Alper

## Mechanics (from commit log, as of 10 May 2026)
- Basic player movement
- Player rotation
- Diving (player leans toward dive direction)
- Basic camera controller
- Cinemachine
- **Drunk Sway** (Allan) — core flavour mechanic
- Car spawning + car movement (obstacle)
- Scene reload on car hit (death/respawn)
- Skybox
- Map in progress (Alper)

## Arcade Cabinet Requirements (must comply)
- Windows build, fullscreen 1920×1080
- Mouse 3 = instant quit
- Auto-quit after 3 min idle (no player input)
- Arcade controller handles ALL input — no mouse, no unmapped keys (causes soft-lock)
- Capsule image required
- Post-jam debugging period available

## Status
Active development — started 8 May 2026, still building as of 10 May 2026.
