const operationsDiv = document.querySelector("#operations");
const generateButton = document.querySelector("#generateButton");
const operationsDiv2 = document.querySelector("#operations2");
const generateButton2 = document.querySelector("#generateButton2");
const operationsDiv3 = document.querySelector("#operations3");
const generateButton3 = document.querySelector("#generateButton3");
const operationsDiv4 = document.querySelector("#operations4");
const generateButton4 = document.querySelector("#generateButton4");
const operationsDiv5 = document.querySelector("#operations5");
const operationsDiv6 = document.querySelector("#operations6");
const operationsDiv7 = document.querySelector("#operations7");
const operationsDiv8 = document.querySelector("#operations8");
const generateButton5 = document.querySelector("#generateButton5");
const operationsDiv9 = document.querySelector("#operations9");

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
        number1 = getRandomNumber(100,999);
        number2 = getRandomNumber(100,999);
        s+=`<div><span>${number1} + ${number2} =<span></div>`;
    }
    s+="</div>";

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(100,999);
        number2 = getRandomNumber(100,999);
        while (number1 === number2) {
            number2 = getRandomNumber(100,999);
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

generateButton2.addEventListener("click", e => {
    operationsDiv2.innerHTML = "";
    let s = "";
    let number1 = "";
    let number2 = "";
    let number3 = "";
    let number4 = "";

    s+=`<div class="division">`;

    s+=`<div class="subdivision1">`;
    for (let i = 0; i < 10; i++) {
        number1 = getRandomNumber(1,20);
        number2 = getRandomNumber(1,20);
        number3 = getRandomNumber(1,10);
        number4 = getRandomNumber(1,10);

        s+=`<div>`;

        
        s+=`<p>
            <span class="frac"><sup>${number1}</sup><span></span><sub>${number3}</sub></span> 
            + 
            <span class="frac"><sup>${number2}</sup><span></span><sub>${number4}</sub></span>
            = 
            <span class="frac longfrac"><sup></sup><span></span><sub></sub></span>
            = 
            <span class="frac shortfrac"><sup></sup><span></span><sub></sub></span>
            </p>`;

        s+=`</div>`;
        
        
    }
    s+=`</div>`;
    
    s+=`<div class="subdivision2">`;
    for (let i = 0; i < 10; i++) {
        number1 = getRandomNumber(1,20);
        number2 = getRandomNumber(1,20);
        number3 = getRandomNumber(1,10);
        number4 = getRandomNumber(1,10);

        s+=`<div>`;

        
        s+=`<p>
            <span class="frac"><sup>${number1}</sup><span></span><sub>${number3}</sub></span> 
            - 
            <span class="frac"><sup>${number2}</sup><span></span><sub>${number4}</sub></span>
            = 
            <span class="frac longfrac"><sup></sup><span></span><sub></sub></span>
            = 
            <span class="frac shortfrac"><sup></sup><span></span><sub></sub></span>
            </p>`;

        s+=`</div>`;
        
    }
    s+=`</div>`;

    s+=`</div>`;

    operationsDiv2.innerHTML = s;
})

generateButton3.addEventListener("click", e => {
    operationsDiv3.innerHTML = "";
    let s = "";
    let number1 = "";
    let number2 = "";
    let number3 = "";
    let number4 = "";
    let number5 = "";
    let number6 = "";

    s+=`<div class="division2">`;

    s+=`<div class="subdivision1">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,10);
        number2 = getRandomNumber(1,10);
        number3 = getRandomNumber(1,10);
        number4 = getRandomNumber(2,7);
        number5 = getRandomNumber(2,7);
        number6 = getRandomNumber(2,7);

        s+=`<div>`;

        
        s+=`<p>
            <span class="frac"><sup>${number1}</sup><span></span><sub>${number4}</sub></span> 
            + 
            <span class="frac"><sup>${number2}</sup><span></span><sub>${number5}</sub></span>
            + 
            <span class="frac"><sup>${number3}</sup><span></span><sub>${number6}</sub></span>
            = 
            <span class="frac longfrac"><sup></sup><span></span><sub></sub></span>
            = 
            <span class="frac shortfrac"><sup></sup><span></span><sub></sub></span>
            </p>`;

        s+=`</div>`;
        
        
    }
    s+=`</div>`;
    

    s+=`<div class="subdivision2">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,10);
        number2 = getRandomNumber(1,10);
        number3 = getRandomNumber(1,10);
        number4 = getRandomNumber(2,7);
        number5 = getRandomNumber(2,7);
        number6 = getRandomNumber(2,7);

        s+=`<div>`;

        
        s+=`<p>
            <span class="frac"><sup>${number1}</sup><span></span><sub>${number4}</sub></span> 
            - 
            <span class="frac"><sup>${number2}</sup><span></span><sub>${number5}</sub></span>
            - 
            <span class="frac"><sup>${number3}</sup><span></span><sub>${number6}</sub></span>
            = 
            <span class="frac longfrac"><sup></sup><span></span><sub></sub></span>
            = 
            <span class="frac shortfrac"><sup></sup><span></span><sub></sub></span>
            </p>`;

        s+=`</div>`;
        
    }
    s+=`</div>`;
    
    s+=`</div>`;

    operationsDiv3.innerHTML = s;




    operationsDiv4.innerHTML = "";
    s = "";

    s+=`<div class="division2">`;

    s+=`<div class="subdivision1">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,10);
        number2 = getRandomNumber(1,10);
        number3 = getRandomNumber(1,10);
        number4 = getRandomNumber(2,7);
        number5 = getRandomNumber(2,7);
        number6 = getRandomNumber(2,7);

        s+=`<div>`;

        
        s+=`<p>
            <span class="frac"><sup>${number1}</sup><span></span><sub>${number4}</sub></span> 
            + 
            <span class="frac"><sup>${number2}</sup><span></span><sub>${number5}</sub></span>
            - 
            <span class="frac"><sup>${number3}</sup><span></span><sub>${number6}</sub></span>
            = 
            <span class="frac longfrac"><sup></sup><span></span><sub></sub></span>
            = 
            <span class="frac shortfrac"><sup></sup><span></span><sub></sub></span>
            </p>`;

        s+=`</div>`;
        
        
    }
    s+=`</div>`;
    

    s+=`<div class="subdivision2">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,10);
        number2 = getRandomNumber(1,10);
        number3 = getRandomNumber(1,10);
        number4 = getRandomNumber(2,7);
        number5 = getRandomNumber(2,7);
        number6 = getRandomNumber(2,7);

        s+=`<div>`;

        
        s+=`<p>
            <span class="frac"><sup>${number1}</sup><span></span><sub>${number4}</sub></span> 
            - 
            <span class="frac"><sup>${number2}</sup><span></span><sub>${number5}</sub></span>
            + 
            <span class="frac"><sup>${number3}</sup><span></span><sub>${number6}</sub></span>
            = 
            <span class="frac longfrac"><sup></sup><span></span><sub></sub></span>
            = 
            <span class="frac shortfrac"><sup></sup><span></span><sub></sub></span>
            </p>`;

        s+=`</div>`;
        
    }
    s+=`</div>`;
    
    s+=`</div>`;

    operationsDiv4.innerHTML = s;







})

generateButton4.addEventListener("click", e => {
    operationsDiv5.innerHTML = "";
    let s = "";
    let number1 = "";
    let number2 = "";

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        s+=`<div><span>${number1} + (+ ${number2}) =<span></div>`;
    }
    s+=`</div>`

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        s+=`<div><span>${number1} + (- ${number2}) =<span></div>`;
    }
    s+=`</div>`

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        s+=`<div><span>${number1} - (+ ${number2}) =<span></div>`;
    }
    s+=`</div>`

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        s+=`<div><span>${number1} - (- ${number2}) =<span></div>`;
    }
    s+=`</div>`

    operationsDiv5.innerHTML = s;





    operationsDiv6.innerHTML="";
    s="";

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        s+=`<div><span>- ${number1} + (+ ${number2}) =<span></div>`;
    }
    s+=`</div>`

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        s+=`<div><span>- ${number1} + (- ${number2}) =<span></div>`;
    }
    s+=`</div>`

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        s+=`<div><span>- ${number1} - (+ ${number2}) =<span></div>`;
    }
    s+=`</div>`

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        s+=`<div><span>- ${number1} - (- ${number2}) =<span></div>`;
    }
    s+=`</div>`

    operationsDiv6.innerHTML = s;



    operationsDiv7.innerHTML="";
    s="";
    let number3 = "";

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(0,30);
        number2 = getRandomNumber(0,30);
        number3 = getRandomNumber(0,30);
        s+=`<div><span>${number1} - ${number2} + ${number3} =<span></div>`;
    }
    s+=`</div>`

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        number3 = getRandomNumber(1,30);
        s+=`<div><span>${number1} + ${number2} - ${number3} =<span></div>`;
    }
    s+=`</div>`

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        number3 = getRandomNumber(1,30);
        s+=`<div><span>${number1} - ${number2} - ${number3} =<span></div>`;
    }
    s+=`</div>`

    operationsDiv7.innerHTML = s;

    

    operationsDiv8.innerHTML="";
    s="";

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(0,30);
        number2 = getRandomNumber(0,30);
        number3 = getRandomNumber(0,30);
        s+=`<div><span>${number1} - (${number2} + ${number3}) =<span></div>`;
    }
    s+=`</div>`

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        number3 = getRandomNumber(1,30);
        s+=`<div><span>${number1} + (${number2} - ${number3}) =<span></div>`;
    }
    s+=`</div>`

    s+=`<div class="container">`;
    for (let i = 0; i < 5; i++) {
        number1 = getRandomNumber(1,30);
        number2 = getRandomNumber(1,30);
        number3 = getRandomNumber(1,30);
        s+=`<div><span>${number1} - (${number2} - ${number3}) =<span></div>`;
    }
    s+=`</div>`

    operationsDiv8.innerHTML = s;

})

generateButton5.addEventListener("click", e => {
    operationsDiv9.innerHTML = "";
    let s = "";
    let number1 = "";
    let number2 = "";
    let number3 = "";

    s+=`<div class="division3">`;

    s+=`<div class="subdivision3">`;
    for (let i = 0; i < 10; i++) {
        number1 = getRandomNumber(2,12);
        number2 = getRandomNumber(1,20);
        number3 = getRandomNumber(1,40);

        s+=`<div>`;

        
        s+=`<p>
            <span>${number1}</span> 
            * 
            <span class="frac"><sup>${number2}</sup><span></span><sub>${number3}</sub></span>
            = 
            <span class="frac longfrac"><sup></sup><span></span><sub></sub></span>
            = 
            <span class="frac shortfrac"><sup></sup><span></span><sub></sub></span>
            </p>`;

        s+=`</div>`;
        
        
    }
    s+=`</div>`;
    
    s+=`<div class="subdivision3">`;
    for (let i = 0; i < 10; i++) {
        number1 = getRandomNumber(1,20);
        number2 = getRandomNumber(1,40);
        number3 = getRandomNumber(2,12);

        s+=`<div>`;

        
        s+=`<p>
            <span class="frac"><sup>${number1}</sup><span></span><sub>${number2}</sub></span> 
            * 
            <span>${number3}</span>
            = 
            <span class="frac longfrac"><sup></sup><span></span><sub></sub></span>
            = 
            <span class="frac shortfrac"><sup></sup><span></span><sub></sub></span>
            </p>`;

        s+=`</div>`;
        
    }
    s+=`</div>`;

    s+=`<div class="subdivision3">`;
    for (let i = 0; i < 10; i++) {
        number1 = getRandomNumber(1,10);
        number2 = getRandomNumber(1,10);
        number3 = getRandomNumber(1,10);
        number4 = getRandomNumber(1,10);

        s+=`<div>`;

        
        s+=`<p>
            <span class="frac"><sup>${number1}</sup><span></span><sub>${number2}</sub></span> 
            * 
            <span class="frac"><sup>${number3}</sup><span></span><sub>${number4}</sub></span>
            = 
            <span class="frac longfrac"><sup></sup><span></span><sub></sub></span>
            = 
            <span class="frac shortfrac"><sup></sup><span></span><sub></sub></span>
            </p>`;

        s+=`</div>`;
        
    }
    s+=`</div>`;

    s+=`</div>`;

    operationsDiv9.innerHTML = s;
})