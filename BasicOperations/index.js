const operationsDiv = document.querySelector("#operations");
const generateButton = document.querySelector("#generateButton");

function getRandomNumber(min, max) {
    return Math.floor(Math.random() * (max - min + 1) ) + min;
}

generateButton.addEventListener("click", e => {
    operationsDiv.innerHTML = "";
    let s = "";
    let number1 = "";
    let number2 = "";
    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(100,9999);
        number2 = getRandomNumber(100,9999);
        s+=`<div><span>${number1} + ${number2} =<span></div>`;
    }
    s+="</div>";

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(100,9999);
        number2 = getRandomNumber(100,9999);
        while (number1 === number2) {
            number2 = getRandomNumber(100,9999);
        }
        if (number2 > number1) {
            let tempNum = number1
            number1 = number2;
            number2 = tempNum;
        }
        s+=`<div><span>${number1} - ${number2} =</span></div>`;
    }
    s+="</div>";

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(100,999);
        number2 = getRandomNumber(10,99);
        s+=`<div><span>${number1} * ${number2} =</span></div>`;
    }
    s+="</div>";

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1000,9999);
        number2 = getRandomNumber(15,50);
        while (number2 % 5 != 0) {
            number2 = getRandomNumber(15,50);
        }
        s+=`<div><span>${number1} : ${number2} =<span></div>`;
    }
    s+="</div>";

    operationsDiv.innerHTML=s;
})

