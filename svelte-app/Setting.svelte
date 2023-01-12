<div class="h-full w-full">
    <div style="background-color:#363e4c; height: 100%;" class="m-auto">
        <div style="background-color:#111727;"class="flex justify-center h-16 p-auto">
            <h1 class="text-2xl text-white m-auto">Settings</h1>
        </div>
        <form method="POST" action="/settings" name="settings">
            <h2 class="text-xl text-white m-auto text-center mt-2">Sound</h2>
            <div style="background-color:#111727;" class="m-6 grid rounded-2xl content-center">
                <label for="large-range" class="block mb-2 text-sm font-medium dark:text-white text-white text-center">Sound <p id="soundValue" class="text-white inline-block">50</p></label>
                <input  id="medium-range" type="range" oninput="soundValue.innerText = this.value"min="0" max="100" value="50" step="1" class="range m-auto w-5/6 h-2 mb-6 rounded-lg ">
            </div>
            <h2 class="text-xl text-white m-auto text-center">Email</h2>
            <div style="background-color:#111727;" class="m-6 grid rounded-2xl content-center">
                <label for="large-range" class="block mb-2 text-sm font-medium dark:text-white text-white text-center">Minimum probability <p id="probabilityValue" class="text-white inline-block">50</p></label>
                <input id="medium-range" type="range" oninput="probabilityValue.innerText = this.value"min="0" max="100" value="50" step="1" class="range m-auto w-5/6 h-2 mb-6 rounded-lg ">
            </div>
            <div style="background-color:#111727;" class="m-6 flex  content-center">
                <label for="large-range" class="block my-2 ml-4 text-sm font-medium dark:text-white text-white text-center w-1/2 ">Minimum priority:</label>
                <select class="block w-1/2 text-center bg-white border focus:outline-none transition-none ">
                    <option class="text-center" value="gunshot">Gunshot</option>
                    <option class="text-center" value="thunder">Thunder</option>
                    <option class="text-center" value="vehicle">Vehicle</option>
                    <option class="text-center" value="animal">Animal</option>
                    <option class="text-center" value="unknown">Unknown</option>
                </select>
            </div>
            <div style="background-color:#111727;" class="m-6 flex  content-center">
                <label for="large-range" class="block my-2 ml-4 text-sm font-medium dark:text-white text-white text-center w-1/2">Select theme :</label>
                <select disabled class="block w-1/2 text-center appearance-none bg-white border focus:outline-none ">
                    <option class="text-center" value="Dark">Dark</option>
                </select>
            </div>
            <div class="flex items-center justify-center ">
                <input id="link-checkbox" type="checkbox" value="" class="w-4 h-4 text-blue-600 bg-gray-100 rounded border-gray-300 focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600">
                <label for="link-checkbox" class="ml-2 text-sm font-medium text-white">Receive email notifications.</label>
            </div>
            <div class="flex align-items justify-content mt-10">
                <button style="background-color:#111727"type="submit" class="  text-white focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm  sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800 border-none m-auto p-auto">Save changes</button>
            </div>
        </form>
    </div>
</div>

<style>
 .range{
    background-color: #fff;
    accent-color: #363e4c;
}

.range::-webkit-slider-thumb{
    width: 20px;
    height: 20px;
    border-radius: 20px;
    box-shadow: -5px 0 0 5px #363e4c, inset 0 0 0 40px #363e4c, inset 0 0 0 40px #363e4c;
    background-color: #fff;
} 
</style>
