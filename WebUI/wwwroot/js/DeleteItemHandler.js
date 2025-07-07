const elementsBlock = document.getElementById("elements-block");

elementsBlock.addEventListener("click", async (event) => {
    if (event.target.matches(".delete-element")) {
        const button = event.target;
        const id = button.dataset.elementId;
        const firstElement = document.querySelectorAll(".element-table").length;
        const response = await DeleteItem(`/delete-bank/bankId/${id}/firstElement/${firstElement}`);

        elementsBlock.insertAdjacentHTML("beforeend", response);
        button.closest(".element-block").remove();
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