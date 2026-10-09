# Source media

`portraits/` holds the un-minified 1024×1024 originals of the faces in
`../portraits/`, which `mancer-ml`'s `bin/sync_assets.ml` cuts to the 256×256
the game draws:

```sh
cd mancer-ml && dune exec ./bin/sync_assets.exe
```

That is the only build step: `cards/`, `backgrounds/`, `sounds/`, `i18n/`,
`ui/`, `campaign/`, `particle/` and `combat/` are committed exactly as the game
reads them, keyed like `cards/<card key>.jpg`, so there is nothing to copy and
no second copy of any file to keep in step.

These originals are tracked with [git-lfs](https://git-lfs.com) (see
`.gitattributes`): the 72 faces are ~98 MB as PNGs and only ~12 MB once cut,
which keeps the repository history small. Clone with `git lfs install` first, or
the files arrive as pointer text.

One file per face, named by id, `portraits/<id>.png`; a source already at 256²
is copied through untouched. The id has to be registered in `mancer-ml`
(`Table.player_portraits`, `Table.duel_opponents`, `Campaign.campaign_portraits`,
`Campaign.npcs`) or nothing will show it. Naming, size and how to paint a face
are in
[mancer-ml/AGENTS.md](https://github.com/minhdoboi/mancer-ml/blob/main/AGENTS.md).

Licensing is the repository's own: AGPL-3.0-or-later, see `../LICENSE`.