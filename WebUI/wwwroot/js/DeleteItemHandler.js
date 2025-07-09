const elementsBlock = document.getElementById("elements-block");
let elementsListCount = parseInt(elementsBlock.dataset.elementsListCount);

elementsBlock.addEventListener("click", async (event) => {
    if (event.target.matches(".delete-element")) {
        const button = event.target;
        const id = button.dataset.elementId;
        const count = document.querySelectorAll(".element-table").length;
        const sortMethod = document.querySelector('input[name="sort"]:checked').value;
        const response = await DeleteItem(`/delete-bank/bank-id/${id}/first-element/${count}/order-method/${sortMethod}`);
         
        elementsBlock.insertAdjacentHTML("beforeend", response);
        button.closest(".element-block").remove();
        elementsListCount --;

        if (elementsListCount == count) {
            loadBanks.style.display = "none";
        }
        if (document.querySelectorAll(".element-block").length === 0) {

        }
    }
});


async function DeleteItem(url) {
    const replacedBank = await fetch(url, {
        method:"POST"
    });

    return await replacedBank.text();
}