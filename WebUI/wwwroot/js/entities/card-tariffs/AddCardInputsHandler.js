const typeRadioButtons = document.querySelectorAll('input[name="Type"]');
const debitType = document.querySelector('input[value="Debit"]');
const creditLimit = document.getElementById("credit-limit");
const interestRate = document.getElementById("interest-rate");
const creditLimitInput = creditLimit.querySelector("input");
const interestRateInput = interestRate.querySelector("input");
const colorInput = document.getElementById("card-color");
const preview = document.querySelector(".color-preview");

preview.style.backgroundColor = colorInput.value;

colorInput.addEventListener("change", () => {
    preview.style.backgroundColor = colorInput.value;
});


typeRadioButtons.forEach(button => {
    button.addEventListener("change", () => {
        if (debitType.checked) {
            creditLimit.style.display = "none";
            interestRate.style.display = "none";
            creditLimitInput.value = 0;
            interestRateInput.value = null;
        }
        else {
            creditLimit.style.display = "block";
            interestRate.style.display = "block";
        }
    })
})
