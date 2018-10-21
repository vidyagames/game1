#!/usr/bin/env bash

set -x

RESULTS_DIR="/tmp/test-results/$TEST_PLATFORM"
RESULTS_PATH="$RESULTS_DIR/results.xml"

mkdir -p "$RESULTS_DIR"

xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' \
  /opt/Unity/Editor/Unity \
    -projectPath WhosAGoodBoy/Who\'s\ A\ Good\ Boy\
    -runTests \
    -testPlatform "$TEST_PLATFORM" \
    -testResults "$RESULTS_PATH" \
    -logFile \
    -batchmode

UNITY_EXIT_CODE=$?

if [ $UNITY_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
elif [ $UNITY_EXIT_CODE -eq 2 ]; then
  echo "Run succeeded, some tests failed";
elif [ $UNITY_EXIT_CODE -eq 3 ]; then
  echo "Run failure (other failure)";
else
  echo "Unexpected exit code $UNITY_EXIT_CODE";
fi

grep test-run $RESULTS_PATH | grep Passed
exit $UNITY_TEST_EXIT_CODE
