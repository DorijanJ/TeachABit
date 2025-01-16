import { expect, Page } from "@playwright/test";

export default async function performLogin(page: Page) {
    await page.goto("https://localhost:3000");

    // Click the login button to open the form
    const loginButton = page.locator("#authForm-prijavaButton");
    await loginButton.click();

    // Wait for form to be visible
    const form = page.locator("#loginForm");
    await expect(form).toBeVisible();

    // Fill in the email/username field
    const emailInput = page.locator('input[name="credentials"]');
    await emailInput.fill("demo");

    // Fill in the password field
    const passwordInput = page.locator('input[name="password"]');
    await passwordInput.fill("Password0");

    // Submit the form
    const submitButton = page.locator("#loginForm-prijavaButton");
    await submitButton.click();

    // Verify successful login by checking for navigationUser element
    const korisnik = page.locator("#navigationUser-korisnik");
    await expect(korisnik).toBeVisible();
}
