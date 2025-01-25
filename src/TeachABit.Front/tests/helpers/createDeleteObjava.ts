import { expect, Page } from "@playwright/test";
import { randomUUID } from "crypto";

export default async function createDeleteObjava(page: Page) {
    await page.goto("https://localhost:3000");

    const naziv = randomUUID();

    const korisnik = page.locator("#navigationUser-korisnik");
    try {
        await expect(korisnik).toBeVisible();
    } catch (ex) {
        throw new Error("Korisnik mora biti ulogiran.");
    }

    const forumButton = page.locator('span:has-text("Forum")');
    await forumButton.click();

    const createObjavaButton = page.locator('button:has-text("Stvori objavu")');
    await createObjavaButton.click();

    const createObjavaForm = page.locator("#objavaEditor");
    await expect(createObjavaForm).toBeVisible();

    const objavaNazivInput = page.locator('input[name="naziv"]');
    await objavaNazivInput.fill(naziv);

    const tiptapEditor = page.locator(".ProseMirror");
    await tiptapEditor.fill("Testing sadrzaj");

    const createObjavaFormButton = page.locator("#objavaEditorStvoriObjavu");
    await createObjavaFormButton.click();

    const createdObjava = page.locator(`#objava:has-text("${naziv}")`);
    await createdObjava.scrollIntoViewIfNeeded();
    await expect(createdObjava).toBeVisible();

    const objavaNavigator = page.locator(
        `#objava:has-text("${naziv}") >> button`
    );
    await objavaNavigator.click();

    const deleteObjavaButton = page.locator("#objavaPage-deleteButton");
    await deleteObjavaButton.click();

    const deletedObjava = page.locator(`#objava:has-text("${naziv}")`);
    await expect(deletedObjava).toHaveCount(0);
}
