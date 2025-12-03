// javascript code
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

  function chooseCharacter() {
    const grid = document.getElementById("charactersGrid");
    if (!grid) return;
  
    grid.innerHTML = ""; // clear before re-render
  
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
        handleCharacterClick?.(character, card)
      );
      grid.appendChild(card);
    });
  }




  /* chooseCharacter();
  
  ---------
    popup.addEventListener("click", function (e) {
      // Close popup if clicking outside the content
      if (!e.target.closest(".popup-content")) {
        closePopup();
      }
    });
--------
    cancelButton.addEventListener("click", () => {
      closePopup();
    });
  
    /* saveButton.addEventListener("click", async function () {
      if (!selectedCharacter || !savedCharacterName) {
        alert("Please select a character and enter a name first.");
        return;
      }
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

        window.location.href = "/Home/Play";
      } catch (error) {
        console.error("Error saving character selection:", error);
        alert("Could not save your character. Please try again.");
      }
    });
  } else {
    console.error("saveButton element not found in the DOM.");
  }

------------
// Close popup when clicking outside the box
    popup.addEventListener("click", function (e) {
      // Close popup if clicking outside the content
      if (!e.target.closest(".popup-content")) {
        closePopup();
      }
    });
  } else {
    console.error("popup element not found in the DOM.");
  }
});  

// doing an innerHTML to generate the cards dynamically, so we dont need to write/repeat it five times
function chooseCharacter() {
  const grid = document.getElementById("charactersGrid");
  if (!grid) return;

  grid.innerHTML = ""; // clear before re-render

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
      handleCharacterClick?.(character, card)
    );
    grid.appendChild(card);
  });
}

function handleCharacterClick(character, cardElement) {
  document.querySelectorAll('.character-card').forEach(card => {
    card.classList.remove('selected');
  });
cardElement.classList.add('selected'); 
selectedCharacter = character; // store the selected character

showPopup(character);
}





function saveCharacterChoice() {
  const nameInput = document.getElementById('characterNameInput');
  const characterName =  nameInput?.value.trim(); // removes space between or after username

  if(!characterName || characterName.length < 2) { // checking for valid username
    alert('Please enter at least 2 characters!');
    nameInput?.focus();
    return;
  }
   savedCharacterName = characterName; // saves the character name

   const payload = {
    characterTemplateId: selectedCharacter.id,
    characterName: characterName,
    templateName: selectedCharacter.name
   };

   closePopup();
   return true;

} */
