document.addEventListener("DOMContentLoaded", function () {
  const continueBtn = document.getElementById("continueBtn");
  const newStoryBtn = document.getElementById("newStoryBtn");
  const popup = document.getElementById("popup");
  const cancelBtn = document.getElementById("cancelBtn");
  const confirmBtn = document.getElementById("confirmBtn");

  // Continue Story button - does nothing for now
  continueBtn.addEventListener("click", function () {
    console.log("Continue Story clicked - no action yet");
    // TODO: Navigate to continue story page
  });

  // New Story button - show popup
  newStoryBtn.addEventListener("click", function () {
    popup.style.display = "flex";
  });

  // Cancel button - hide popup
  cancelBtn.addEventListener("click", function () {
    popup.style.display = "none";
  });

  // Confirm button - go to character selection
  confirmBtn.addEventListener("click", function () {
    // TODO: Call API to delete save data
    window.location.href = "/Home/Character";
  });

  // Close popup when clicking outside
  popup.addEventListener("click", function (e) {
    if (e.target === popup) {
      popup.style.display = "none";
    }
  });
}); //end of code
