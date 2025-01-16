import { expect, Page } from "@playwright/test";
import { randomUUID } from "crypto";

export default async function createDeleteRadionica(page: Page) {
    await page.goto("https://localhost:3000");

    const naziv = randomUUID();

    const korisnik = page.locator("#navigationUser-korisnik");
    try {
        await expect(korisnik).toBeVisible();
    } catch (ex) {
        throw new Error("Korisnik mora biti ulogiran.");
    }

    const forumButton = page.locator('span:has-text("Radionice")');
    await forumButton.click();

    const createRadionicaButton = page.locator('button:has-text("Započni novu radionicu")');
    await createRadionicaButton.click();

    const createRadionicaForm = page.locator("#radionicaEditor");
    await expect(createRadionicaForm).toBeVisible();

    const radionicaNazivInput = page.locator('input[name="naziv"]');
    await radionicaNazivInput.fill(naziv);

    const radionicaOpisInput = page.locator('textarea[name="opis"]');
    await radionicaOpisInput.fill("Testing sadrzaj");

    const radionicaCijenaInput = page.locator('input[name="radionica-cijena"]');
    await radionicaCijenaInput.fill("5");

    const createRadionicaFormButton = page.locator("#stvoriRadionicuButton");
    await createRadionicaFormButton.click();

    const createdRadionica = page.locator(`#radionica:has-text("${naziv}")`);
    await createdRadionica.scrollIntoViewIfNeeded();
    await expect(createdRadionica).toBeVisible();

    const radionicaNavigator = page.locator(
        `#radionica:has-text("${naziv}") >> button`
    );
    await radionicaNavigator.click();

    // const deleteObjavaButton = page.locator("#objavaPage-deleteButton");
    // await deleteObjavaButton.click();

    // const deletedObjava = page.locator(`#objava:has-text("${naziv}")`);
    // await expect(deletedObjava).toHaveCount(0);
}
