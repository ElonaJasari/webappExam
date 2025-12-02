// javascript code
const characters = [
  {
    id: 1,
    name: "Eiven Nordflamme",
    description: "Calm, grounded, quiet clumsy",
    dialog: "....",
    imageUrl: "/images/cool_dude.png", // fix path
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

function showPopup(character) {
  const popup = document.getElementById('characterPopup');
  const popupImage = document.getElementById('popupCharacterImage');
  const popupName = document.getElementById('popupCharacterName');
  const popupDescription = document.getElementById('popupCharacter');
  const nameInput = document.getElementById('nameInput');

  if(!popup) {
    console.error('There is an error');
    return;
  }

  if (popupImage) {
    popupImage.src = character.imageUrl || '';
    popupImage.alt = character.name;
  }

  if (popupName) {
    popupName.textContent = character.name;
  }

  if (popupDescription) {
    popupDescription.textContent = character.description || '';
  }

  if (nameInput) {
    nameInput.value = '';
    nameInput.placeholder = character.name;
    nameInput.focus();
  }

  popup.style.display = 'flex';
  document.body.style.overflow = 'hidden';

}

function closePopup() {
  const popup = document.getElementById('characterPopup');
  if (popup) {
    popup.style.display = 'none';
    document.body.style.overflow = '';
  }
}

closePopup();