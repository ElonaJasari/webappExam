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
    name: "Character 4",
    description: "....",
    dialog: "....",
    imageUrl: "",
  },
  {
    id: 5,
    name: "Character 5",
    description: "....",
    dialog: "....",
    imageUrl: "",
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
