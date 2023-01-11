import { render, screen, fireEvent } from "@testing-library/svelte";
import Login from "../svelte-app/Login.svelte";

describe("UpdateUserForm should have input fields for all user fields", () => {   
  test("check if the component has input fields for all user fields", async () => {
    const { container } = render(Login);

    let emailInput = container.getElementsByTagName("input")[0];
    let passwordInput = container.getElementsByTagName("input")[1];

    expect(emailInput.placeholder.toLowerCase()).toContain("mail")
    expect(passwordInput.placeholder.toLowerCase()).toContain("password")

    expect(emailInput).toBeInTheDocument()
    expect(passwordInput).toBeInTheDocument()
  });
});