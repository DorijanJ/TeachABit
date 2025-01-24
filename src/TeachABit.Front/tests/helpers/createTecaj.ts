import { expect, Page } from "@playwright/test";
export default async function createTecaj(page: Page) {
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
}