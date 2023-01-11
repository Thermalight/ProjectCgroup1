import { render, screen, fireEvent } from "@testing-library/svelte";
import UpdateUserForm from "../svelte-app/UpdateUserForm.svelte";

describe("UpdateUserForm should have input fields for all user fields", () => {   
  test("check if the component has input fields for all user fields", async () => {
    const { container } = render(UpdateUserForm, { props: { user: {username:"test"}} });

    const hidden = container.getElementsByTagName("Input")[0];
    const username = container.getElementsByTagName("Input")[1];
    const password = container.getElementsByTagName("Input")[2];
    const email = container.getElementsByTagName("Input")[3];
    
    expect(hidden).toBeInTheDocument()
    expect(username).toBeInTheDocument()
    expect(password).toBeInTheDocument()
    expect(email).toBeInTheDocument()

    expect(hidden.getAttribute("type")).toBe("hidden")
    expect(username.getAttribute("type")).toBe("text")
    expect(password.getAttribute("type")).toBe("password")
    expect(email.getAttribute("type")).toBe("email")
  });
});