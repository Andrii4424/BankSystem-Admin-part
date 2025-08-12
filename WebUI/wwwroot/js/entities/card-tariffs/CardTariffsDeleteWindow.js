import { GeneralListMethods } from "../../GeneralListMethods.js";

const deleteButton = document.querySelector(".delete");

document.body.addEventListener("click", async (event) => {
    if (event.target.matches(".confirm-delete-button")) {       //Deleting
        const id = deleteButton.dataset.elementId;
        await DeleteItem(`/delete-card-with-redirect/${id}`);
    }
    else if (event.target.matches(".delete")) {                 //Open delete confitmation window
        GeneralListMethods.OpenConfirmDeleteWindow(`${deleteButton.dataset.elementName} card`, `\u043A\u0430\u0440\u0442\u043A\u0443 ${deleteButton.dataset.elementName}`);
    }
    else if (!event.target.closest("#delete-window")) {         //Close delete confitmation window when user click on another part of the screen
        GeneralListMethods.CloseConfirmDeleteWindow();
    }
    else if (event.target.matches(".cancel-delete-button")) {   //Close delete confitmation window when user click close
        GeneralListMethods.CloseConfirmDeleteWindow();
    }
});

async function DeleteItem(url) {
    await fetch(url, {
        method: "POST"
    });
    GeneralListMethods.CloseConfirmDeleteWindow();
    window.location.href = "/cards";
}
