import { render, screen } from '@testing-library/svelte';
import App from '../svelte-app/App.svelte';

test("says 'Hello world!'", () => {
    render(App, {props: {name: "world"}});
    const node = screen.queryByText("Hello world!");
    expect(node).not.toBeNull();
})