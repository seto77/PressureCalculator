# PressureCalculator

PressureCalculator는 고압 실험(다이아몬드 앤빌 셀 실험 등)에서 압력을 결정하기 위한 무료 Windows 애플리케이션입니다. 서로 보완적인 세 가지 방법을 지원합니다.

- **[루비 형광](1-ruby-fluorescence.md)** : 루비 R1 형광선의 이동으로부터 압력을 구합니다. 여러 발표된 루비 스케일과 온도 보정을 지원합니다.
- **[다이아몬드 라만 에지](2-diamond-raman.md)** : 다이아몬드 앤빌 라만 밴드의 고파수 쪽 에지로부터 압력을 구합니다.
- **[상태방정식 (EOS)](3-equation-of-states.md)** : 금, 백금, NaCl, 페리클레이스 등 표준 물질의 격자 상수(또는 단위포 부피) 측정값으로부터 압력을 구합니다.

측정 스펙트럼은 텍스트 파일에서 불러와 애플리케이션 안에서 바로 평활화·피팅할 수 있습니다([스펙트럼과 피팅](4-spectra-and-fitting.md) 참조).

![메인 창 (루비 형광 모드)](../assets/cap-ko-auto/FormMain-ruby.png){width=700px}

## 설치

최신 버전은 [GitHub 릴리스 페이지](https://github.com/seto77/PressureCalculator/releases/latest)에서 내려받을 수 있습니다.

| 파일 | 설명 |
|---|---|
| `PressureCalculator-setup.msi` | **권장.** 일반 (x64) Windows PC용 설치 프로그램. |
| `PressureCalculator-setup_arm64.msi` | Windows on Arm(Snapdragon 탑재 PC, 가상화로 Windows를 실행하는 Apple Silicon Mac 등)용 설치 프로그램. |
| `PressureCalculator-v.X.zip` | 포터블 버전 (x64): 설치 불필요, 자체 완결형. 관리자 권한이 없는 PC에 적합합니다. |
| `PressureCalculator-v.X_arm64.zip` | Windows on Arm용 포터블 버전. |

MSI 설치 프로그램을 실행하려면 .NET Desktop Runtime 10이 필요합니다. 설치되어 있지 않으면 첫 실행 시 다운로드 링크가 포함된 대화 상자가 표시됩니다. 포터블 ZIP 패키지에는 런타임이 포함되어 있어 별도 설치가 필요 없습니다. 쓰기 가능한 폴더에 ZIP을 풀고 `PressureCalculator.exe`를 실행하기만 하면 됩니다.

PressureCalculator는 사용자 단위로 설치되며(관리자 권한 불필요), 설정은 `HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator`에 저장됩니다.

## 표시 언어

사용자 인터페이스는 11개 언어를 지원합니다. 메뉴 모음의 **Language**에서 언어를 선택하면 PressureCalculator가 새 언어로 다시 시작됩니다. 애플리케이션에서 연 경우 이 온라인 매뉴얼도 같은 언어 선택을 따릅니다.

## 온라인 도움말

애플리케이션에서 ++f1++ 키를 누르거나 **도움말 → 온라인 매뉴얼**을 선택하면 현재 모드에 해당하는 매뉴얼 페이지가 열립니다.
