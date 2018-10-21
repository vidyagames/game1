#!/usr/bin/env bash

set -e
set -x
mkdir -p /root/.cache/unity3d
mkdir -p /root/.local/share/unity3d/Unity/
set +x
echo 'Decoding and writing $UNITY_LICENSE_CONTENT_ENCODED to license file /root/.local/share/unity3d/Unity/Unity_lic.ulf'
echo "$UNITY_LICENSE_CONTENT_ENCODED" | base64 --decode | tr -d '\r' > /root/.local/share/unity3d/Unity/Unity_lic.ulf
