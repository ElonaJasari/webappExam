const characters = [
  {
    id: 1,
    name: "Eiven Nordflamme",
    description: "Calm, grounded, quiet clumsy",
    dialog: "....",
    imageUrl: "/images/cool_dude.png",
  },
  {
    id: 2,
    name: "Vargár Ravdna",
    description: "<i>HIMOTHY</i>",
    dialog: "...",
    imageUrl: "/images/confident_dude.png",
  },
  {
    id: 3,
    name: "TUNG TUNG SAMUR",
    description: "TUNG TUNG THE GREAT",
    dialog: "....",
    imageUrl: "/images/tung_tung.png",
  },
  {
    id: 4,
    name: "Aurora Borealis",
    description: "caring, fearless, strong",
    dialog: "....",
    imageUrl: "/images/awsome_girl.png",
  },
  {
    id: 5,
    name: "Chloe Kelly",
    description: "Pressure? What pressure?",
    dialog: "....",
    imageUrl: "/images/chloekelly.png",
  },
];

let selectedCharacter = null;
let savedCharacterName = null;

document.addEventListener("DOMContentLoaded", () => {
  chooseCharacter();
  setupPopupHandlers();

  function chooseCharacter() {
    const grid = document.getElementById("charactersGrid");
    if (!grid) return;

    grid.innerHTML = "";

    characters.forEach((character) => {
      const card = document.createElement("div");
      card.className = "character-card";
      card.innerHTML = `
        <div class="character-image-container">
          <img class="character-image" src="${character.imageUrl}" alt="${character.name}">
        </div>
        <div class="character-info">
          <h3 class="character-name">${character.name}</h3>
          <p class="character-description">${character.description}</p>
        </div>`;
      card.addEventListener("click", () =>
        handleCharacterClick(character, card)
      );
      grid.appendChild(card);
    });
  }

  function handleCharacterClick(character, cardElement) {
    document.querySelectorAll(".character-card").forEach((card) => {
      card.classList.remove("selected");
    });
    cardElement.classList.add("selected");
    selectedCharacter = character;
    showPopup(character);
  }

  function showPopup(character) {
    const popup = document.getElementById("characterPopup");
    const popupImage = document.getElementById("popupCharacterImage");
    const popupName = document.getElementById("popupCharacterName");
    const popupDesc = document.getElementById("popupCharacterDescription");
    const nameInput = document.getElementById("characterNameInput");

    if (!popup) return;

    // Populate popup with character data
    if (popupImage) popupImage.src = character.imageUrl;
    if (popupName) popupName.textContent = character.name;
    if (popupDesc) popupDesc.textContent = character.description;
    if (nameInput) {
      nameInput.value = "";
      nameInput.placeholder = character.name; // Set character name as placeholder
    }

    popup.style.display = "flex";
  }

  function closePopup() {
    const popup = document.getElementById("characterPopup");
    const nameInput = document.getElementById("characterNameInput");

    if (popup) popup.style.display = "none";
    if (nameInput) nameInput.value = ""; // Clear input when closing
  }

  function setupPopupHandlers() {
    const popup = document.getElementById("characterBtn");
    const closeBtn = document.getElementById("closeBtn");
    const cancelBtn = document.getElementById("cancelButton");
    const saveBtn = document.getElementById("saveButton");

    // Close button (X)
    if (closeBtn) {
      closeBtn.addEventListener("click", closePopup);
    }

    // Cancel button
    if (cancelBtn) {
      cancelBtn.addEventListener("click", closePopup);
    }

    // Click outside popup to close
    if (popup) {
      popup.addEventListener("click", function (e) {
        if (e.target === popup) {
          closePopup();
        }
      });
    }

    // Save button
    if (saveBtn) {
      saveBtn.addEventListener("click", async function () {
        const nameInput = document.getElementById("characterNameInput");
        const characterName =
          nameInput?.value.trim() || nameInput?.placeholder || ""; // placeholder if name is not set

        if (!characterName || characterName.length < 2) {
          alert("Please enter at least 2 characters!");
          nameInput?.focus();
          return;
        }

        if (!selectedCharacter) {
          alert("Please select a character first.");
          return;
        }

        savedCharacterName = characterName;

        try {
          const response = await fetch("/api/CharacterSelection", {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              CharacterId: selectedCharacter.id,
              CustomName: savedCharacterName,
            }),
          });

          if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`API Error: ${response.status} - ${errorText}`);
          }

          window.location.href = "/Story/Play";
        } catch (error) {
          console.error("Error saving character selection:", error);
          alert("Could not save your character. Please try again.");
        }
      });
    }

    // ESC key to close popup
    document.addEventListener("keydown", function (e) {
      if (e.key === "Escape") {
        closePopup();
      }
    });
  }
});
