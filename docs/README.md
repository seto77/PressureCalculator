# docs

- `mkdocs.yml`, `src/` — published online manual (MkDocs Material, 11 languages; deployed to <https://seto77.github.io/PressureCalculator/> by `.github/workflows/pages.yml`).
- `drafts/` — **drafts written by Claude from the source code (2026-11-05), not yet reviewed by the author.** They live outside `docs_dir` (`src/`), so they are not built or published. Japanese and English files with the same name correspond to each other.

## drafts/ 目次 / Contents

| # | 日本語 | English | 内容 / Content |
|---|---|---|---|
| 00 | [既存マニュアルと現行コードの突き合わせ](drafts/ja/00-manual-vs-code.md) | [Existing manual vs. current code](drafts/en/00-manual-vs-code.md) | 食い違い一覧・マニュアルに無い機能 / conflicts and undocumented features |
| 01 | [利用マニュアル](drafts/ja/01-user-manual.md) | [User manual](drafts/en/01-user-manual.md) | 画面一覧・操作手順・設定項目・既定値 / windows, procedures, settings, defaults |
| 02 | [圧力スケール一覧](drafts/ja/02-pressure-scales.md) | [Pressure scales](drafts/en/02-pressure-scales.md) | 式・パラメータ（行番号付き）・出典・適用範囲・計算値 / formulae, parameters with line numbers, sources, ranges, computed values |
| 03 | [実装上の疑問点](drafts/ja/03-implementation-questions.md) | [Implementation questions](drafts/en/03-implementation-questions.md) | 誤りの疑い（コードは未変更） / suspected problems (code unchanged) |
| — | [tools/](drafts/tools/README.md) | | C# ハーネスと Python 独立実装 / C# harness and independent Python implementation |
