<script>
    import goToPage from "./goToPage.js";
    import Banner from "./Banner.svelte";

    if (localStorage.getItem("token")) {
        goToPage("dashboard");
    }

    let seePassword = false;
    $: type = seePassword ? 'text' : 'password'
    let seeError = "hidden";

    let state = {
        email: '',
        password: ''
    }

    function onInput (event) {
	    state.password = event.target.value
    }

    function submit() {
        fetch("https://" + window.location.host + "/login", {
            method: "POST",
            body: JSON.stringify(state),
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
        }).then(response => {
            if (response.status == 200) {
                response.json().then(data => {
                    // get the token from the body of the response
                    localStorage.setItem("token", data);
                    goToPage("dashboard");
                });
            } else {
                seeError = "block";
            }
        });
    }
</script>
<style>
    a:active {
    color: gray;
    }
    a {
    color: white;
    }
    .customButton:active{
        background-color: transparent;
    }
</style>
<Banner/>
<div class="h-full">
    <div style="background-color:#363e4c;"class="block  h-4/6 w-5/6 m-auto rounded-2xl">
        <div style="background-color:#111727;"class="flex justify-center   h-16 rounded-t-2xl p-auto">
            <h1 class="text-2xl text-white m-auto">Log in</h1>
        </div>
        <div style="height: 89%;"class="grid w-4/5 content-center m-auto py-16">
            <div role="alert" class="{seeError}">
                <div class="border border-t-0 border-red-400 rounded-lg bg-red-100  text-red-700 text-center">
                  <p>Wrong credentials.</p>
                </div>
            </div>
            <form autocomplete="off" method="POST" name="login" id="login">
                <div class="flex relative z-0 mb-6 w-full group">
                    <span class="material-symbols-outlined m-auto text-white">mail</span>
                    <input bind:value={state.email} type="email" name="email" id="email" class="block  w-full text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer h-9 ml-3" placeholder="Email address" required>
                </div>
                
                <div class="flex relative z-0 mb-6 w-full group">
                    <span class="material-symbols-outlined m-auto text-white">lock</span>
                    <input { type } name="password" on:input={ onInput } id="password" class=" text-white ml-3 mr-1 block  w-full text-sm  bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer" placeholder="Password" required>
                    <button type="button" class="btn border-none customButton" on:click="{ () => seePassword = !seePassword }">
                        {#if seePassword}
                            <!-- see -->
                            <span class="material-symbols-outlined m-auto text-white">visibility</span>
                        {:else}
                            <!-- unsee -->
                            <span class="material-symbols-outlined m-auto text-white">visibility_off</span>
                        {/if}
                    </button>
                </div>
                <div class="flex align-items justify-content mt-10">
                    <button style="background-color:#111727"type="submit" class="  text-white focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm  sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800 border-none m-auto p-auto" on:click|preventDefault={submit}>Submit</button>
                </div>
            </form>
        </div>
    </div>
</div>