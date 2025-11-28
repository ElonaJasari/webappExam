// javascript code
const characters = [ // fix db later
    {
    id: 1,
    name: "Character 1",
    description: "....",
    dialog: "....",
    imageUrl: ""
},
{
    id: 2,
    name: "Character 2",
    description: "....",
    dialog: "....",
    imageUrl: ""
},
{
    id: 3,
    name: "Character 3",
    description: "....",
    dialog: "....",
    imageUrl: ""
},
{
    id: 4,
    name: "Character 4",
    description: "....",
    dialog: "....",
    imageUrl: ""
},
{
    id: 5,
    name: "Character 5",
    description: "....",
    dialog: "....",
    imageUrl: ""
}
];

let selectedCharacter = null;
let savedCharacterName = null;

document.addEventListener('DOMContentLoaded', () => {
    chooseCharacter();
})

// doing an innerHTML to generate the cards dynamically, so we dont need to write/repeat it five times
function chooseCharacter(){
    const grid = document.getElementById('charactersGrid');

    characters.forEach(character => {
        const card = document.createElement('div');
        card.className = 'character-card';

        card.innerHTML = `
            <div class="character-image-container">
                <img class="character-image" src="${character.imageUrl}" alt="${character.name}">
                </div>
            <div class="character-info">
                <h3 class="character-name">${character.name}</h3>
                <p class="character-description">${character.description}</p>
            </div> `;

            card.addEventListener('click', () => handleCharacterClick(character, card));
            grid.appendChild(card);

    });

}