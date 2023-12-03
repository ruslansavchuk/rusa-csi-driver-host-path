# CSI hostpath driver

## Table of Contents

1. [Overview](#overview)
2. [Functionality](#functionality)
3. [Getting Started](#getting-started)
   - [Installation](#installation)
   - [Usage](#usage)
4. [License](#license)

## Overview

Hostpath CSI Driver is a Kubernetes Container Storage Interface (CSI) implementation, designed to facilitate storage management in Kubernetes clusters. It provides dynamic volume provisioning and supports ephemeral volumes for various use cases.

## Functionality

The driver provides empty directories that are backed by the same filesystem as EmptyDir volumes.

## Getting Started

### Installation

```bash
mkdir csi-hostpath && cd csi-hostpath
RELEASE=$(curl https://api.github.com/repos/ruslansavchuk/rusa-csi-driver-host-path/releases/latest | grep tag_name | awk -F'"' '{print $4}')
wget "https://github.com/ruslansavchuk/rusa-csi-driver-host-path/releases/download/$RELEASE/rusa-csi-driver-host-path.tar.gz"
tar -xzvf rusa-csi-driver-host-path.tar.gz
helm upgrade --install -n test --create-namespace csi-driver-hostpath ./rusa-csi-driver-host-path --debug
```

### Usage

```bash
kubectl apply -n rusa -f - <<EOF
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: csi-hostpath-pvc
spec:
  accessModes:
    - ReadWriteOnce
  resources:
    requests:
      storage: 100Mi

---
apiVersion: v1
kind: Pod
metadata:
  name: my-pod
spec:
  containers:
    - name: nginx
      image: nginx
      volumeMounts:
        - name: csi-hostpath-v
          mountPath: /path/to/mount
  volumes:
    - name: csi-hostpath-v
      persistentVolumeClaim:
        claimName: csi-hostpath-pvc
EOF
```

## License

This project is licensed under the MIT License.
