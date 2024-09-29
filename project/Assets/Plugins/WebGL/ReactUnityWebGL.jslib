mergeInto(LibraryManager.library, {
  GameOver: function (time, score) {
    dispatchReactUnityEvent("GameOver", time, score);
  },
});

mergeInto(LibraryManager.library, {
  RequestTokenBalanceUpdate: function (time, score) {
    dispatchReactUnityEvent("RequestTokenBalanceUpdate", time, score);
  },
});
