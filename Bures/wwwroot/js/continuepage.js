document.addEventListener("DOMContentLoaded", function () {
  const continueBtn = document.getElementById("continueBtn");
  const newStoryBtn = document.getElementById("newStoryBtn");
  const popup = document.getElementById("popup");
  const cancelBtn = document.getElementById("cancelBtn");
  const confirmBtn = document.getElementById("confirmBtn");

  // Continue Story button - start at the beginning of the current act
  continueBtn.addEventListener("click", function () {
    window.location.href = "/Story/StartOfAct";
  });

  // New Story button - show popup
  newStoryBtn.addEventListener("click", function () {
    popup.style.display = "flex";
  });

  // Cancel button - hide popup
  cancelBtn.addEventListener("click", function () {
    popup.style.display = "none";
  });

  // Confirm button - reset progress and go to character selection
  confirmBtn.addEventListener("click", function () {
    window.location.href = "/Story/ResetAndStart";
  });

  // Close popup when clicking outside
  popup.addEventListener("click", function (e) {
    if (e.target === popup) {
      popup.style.display = "none";
    }
  });
});
