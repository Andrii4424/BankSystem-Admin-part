const typeRadioButtons = document.querySelectorAll('input[name="Type"]');
const debitType = document.querySelector('input[value="Debit"]');
const creditLimit = document.getElementById("credit-limit");
const interestRate = document.getElementById("interest-rate");
const creditLimitInput = creditLimit.querySelector("input");
const interestRateInput = interestRate.querySelector("input");


typeRadioButtons.forEach(button => {
    button.addEventListener("change", () => {
        if (debitType.checked) {
            creditLimit.style.display = "none";
            interestRate.style.display = "none";
        }
        else {
            creditLimit.style.display = "block";
            interestRate.style.display = "block";
            creditLimitInput.value = 0;
            interestRateInput.value = null;
        }
    })
})
