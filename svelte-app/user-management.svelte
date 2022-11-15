<script>
    import SearchUser from "./SearchUser.svelte";
    export async function loadUsers () { 
        const response = await fetch("https://localhost/users", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        });
        return await response.json();
    }
</script>
<SearchUser/>
