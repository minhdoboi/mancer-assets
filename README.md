Repository for mancer assets.

The media the game loads, committed in the shape it loads them under the key it
looks up: `cards/<card key>.jpg`, `backgrounds/<house key>.jpg`,
`portraits/<id>.png`, `sounds/<sound key>.mp3`, plus `i18n/`, `ui/`,
`campaign/`, `particle/`, `combat/`, `fonts/` and `shaders/`.

`source/portraits/` is the one source tree: the 1024² originals of the faces,
cut to 256² by `mancer-ml`'s `bin/sync_assets.exe` and tracked with
[git-lfs](https://git-lfs.com). See `source/README.md`.

Licensed under [AGPL-3.0-or-later](LICENSE) (see also
[mancer-ml/LICENSE](https://github.com/minhdoboi/mancer-ml/blob/main/LICENSE)).
Use and redistribution require open source–compatible terms as described
there.