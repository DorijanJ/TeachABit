import { expect, Page } from "@playwright/test";
export default async function createKomentarTecaj(page: Page){
    await page.goto("https://localhost:3000");
    const korisnik = page.locator("#navigationUser-korisnik");
    try {
        await expect(korisnik).toBeVisible();
    } catch (ex) {
        throw new Error("Korisnik mora biti ulogiran.");
    }
    const Button1 = page.locator('div.MuiButtonBase-root.MuiListItemButton-root.MuiListItemButton-gutters._navButton_1swuc_9._activeItem_1swuc_19');
    await Button1.click();

    const Button2 = page.locator('button:has-text("Stvori tečaj")')
    await Button2.click();

    const createCekajForm = page.locator('div.MuiDialog-container[role="presentation"]');
    await expect(createCekajForm).toBeVisible();

    const inputField = page.locator('input[name="naziv"]');
    await inputField.fill("Tecaj1");

    const inputField1 = page.locator('div[contenteditable="true"].ProseMirror')
    await inputField1.fill("Opis1");

    const inputField2 = page.locator('input[name="cijena"]');
    await inputField2.fill("100");

    const Button3 = page.locator('button.MuiButtonBase-root.MuiButton-root.MuiButton-contained.MuiButton-containedPrimary.MuiButton-sizeMedium.MuiButton-containedSizeMedium.MuiButton-colorPrimary.MuiButton-root.MuiButton-contained.MuiButton-containedPrimary.MuiButton-sizeMedium.MuiButton-containedSizeMedium.MuiButton-colorPrimary._myButton_183jv_21')
    await Button3.click()

    await page.waitForTimeout(10000)
    const addCommentButton = page.locator('button:has-text("Dodaj Komentar")');
    await addCommentButton.click();

    const editor = page.locator('div[contenteditable="true"]');
    await editor.fill("tekst");

    const button = page.locator('button:has-text("Stvori komentar")');
    await button.click();


    /*
    const addButton = page.locator('button.MuiButtonBase-root.MuiIconButton-root.MuiIconButton-sizeMedium.css-hefddw');
    await addButton.click();

    const label = page.locator('label:has-text("Naziv")');
    await expect(label).toBeVisible();

    const inputField3 = page.locator('input[name="naziv"]');
    await inputField3.fill('Lekcija_1');

    const editor = page.locator('div[contenteditable="true"]');
    await editor.fill("Objasnjenje");

    const button = page.locator('button:has-text("Stvori lekciju")');
    await button.click();
    */

}