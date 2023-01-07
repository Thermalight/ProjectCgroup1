import { render, screen, fireEvent } from "@testing-library/svelte";
import User from "../svelte-app/User.svelte";

describe("The user should have fields to display information", () => {   
  test("check if all user fields are displayed for a given user", async () => {
    const { container } = render(User, { props: { user: {username:"test"}} });

    const username = container.getElementsByTagName("p")[0];
    const email = container.getElementsByTagName("p")[1];
    const admin = container.getElementsByTagName("p")[2];
    const subscribed = container.getElementsByTagName("p")[3];
    
    expect(username).toBeInTheDocument()
    expect(admin).toBeInTheDocument()
    expect(email).toBeInTheDocument()
    expect(subscribed).toBeInTheDocument()

    expect(username.textContent).toContain("username")
    expect(email.textContent).toContain("email")
    expect(admin.textContent).toContain("admin")
    expect(subscribed.textContent).toContain("subscribed")
  });
});