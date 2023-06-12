# QUASAR+VUE3+VITE

- AXIOS
- PINIA
- TYPESCRIPT
- VUE-ROUTER
- ECHARTS

### PLATFORM

- [x] Web Application (quasar electron)
- [x] Mobile Application (quasar cordova)
- [x] Progressive Web App (quasar pwa)

### PACKAGE MANAGER

- [x] NPM
- [x] YARN (quasar's recommended)
- [x] PNPM (used in this project)

## Install the dependencies

```bash
yarn
# or
npm install
# or
pnpm install
```

### Start the app in development mode (hot-code reloading, error reporting, etc.)

```
-m means --mode
-t means --target
```

```bash
# normal
quasar dev

# mode-pwa
quasar dev -m pwa

# mode-electron
quasar dev -m electron

# mode-cordova
### 1. You should be installed JAVA-SDK(1.8.0↑), Android Studio SDK (30.0.3 from tools)
### 2. Finally, an environment variable must be added.
### Alternatively, connect the device to your PC and it will run.
quasar dev -m [android | ios]
```

### Build the app for production

```bash
# normal
quasar build

# build-pwa
quasar build -m pwa

# build-electron
quasar build -m electron

# build-cordova
quasar build -m [android | ios]
```
