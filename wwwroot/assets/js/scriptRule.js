const roulette = document.querySelector("#roulette");
const spinButton = document.querySelector("#spin");
const resetButton = document.querySelector("#reset");

const maxSpins = 10;
const minSpins = 1;

const maxDegrees = 360;
const minDegrees = 1;

const getRandomNumber = (min, max) => {
    return Math.round(Math.random() * (max - min) + min);
}

spinButton.addEventListener("click", () => {

    const spins = getRandomNumber(minSpins, maxSpins);
    const degrees = getRandomNumber(minDegrees, maxDegrees);

    const fullSpins = (spins - 1) * 360;
    const spin = fullSpins + degrees; //si son 5 vueltas va durar 5 segundos

    const animationTime = spins;
    
    roulette.style.transform = `rotate(${spin}deg)`; //equivale a 5 s

   

 

    /*roulette.style.transitionDuration = `${animationTime}s`;*/
    roulette.style.transitionDuration = `20s`;

    spinButton.style.display = "none";
    resetButton.style.display = "inline-block";

});

resetButton.addEventListener("click", () => {

    roulette.style.transform = "rotate(0deg)";
    roulette.style.transitionDuration = "2s";
    spinButton.style.display = "inline-block";
    resetButton.style.display = "none";


});