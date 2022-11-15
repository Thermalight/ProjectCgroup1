<script>
    export let user;
</script>
{#if user}
    <div>
        <div class="content text-white">
            <p>username: {console.log(user)}</p>
            <p>email: {user.Email}</p>
            <p>admin: {user.IsAdmin ? "True" : "False"}</p>
            <p>subscribed: {user.Subscriber ? "True" : "False"}</p>
        </div>
    </div>
{/if}