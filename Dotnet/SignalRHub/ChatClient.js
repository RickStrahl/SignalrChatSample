var app = null;
var vm = {
    hub: null,
    serverUrl: "http://signalrswf.west-wind.com/",
    //serverUrl: "http://localhost/signalRHub/",
    group: "Southwest Fox",
    name: "Anonymous" + new Date().getSeconds(),
    message: "",
    groups: [
        "Southwest Fox",
        "Web Connection",
        "Markdown Monster",
        "Foyer"
    ],
    messages: [],
    forum: "Southwest Fox",
    initialize: function() {
        vm.hub = $.connection.chatHub;

        // connecting to our external server        
        vm.hub.connection.url = vm.serverUrl + "signalr";

        vm.hub.client.OnReceiveMessage = vm.onReceiveMessage;
        $.connection.hub.start().done(function () {
            vm.joinGroup();

            // Initialize Vue js for databinding
            app = new Vue({
                el: "#Page",
                data: vm
            });
        });

        $(document.body).on("click", ".message-item a", function () {
            window.open(this.href);
            return false;
        });

        function resize() {
            var height = $(window).height();
            console.log(height);
            $("#ChatMessages").height(height - 320);
        }
        resize();
        $(window).on("resize",debounce(resize,5));
    },
    joinGroup: function() {
        vm.hub.server.joinGroup(vm.name, vm.group);
        vm.sendMessage(vm.name + " joined " + vm.group);
    },
    sendMessage: function(message, group, name) {
        if (!message)
            return;
        if (!group)
            group = vm.group;
        if (!name)
            name = vm.name;
        vm.hub.server.sendMessage(message, group, name);

        vm.message = "";
    },
    onReceiveMessage: function (msg) {
   
        // fix up message with Markdown
        msg.Message = marked(msg.Message);

        vm.messages.push(msg);
        if (vm.messages > 30)
            vm.messages.splice(0, 10);

        vm.scrollBottom();
        vm.highlightCode();
    },
    clearMessages: function () {
        vm.messages = [];
    },
    highlightCode: function () {
        setTimeout(function () {
            $("pre code")
                .each(function (i, block) {
                    hljs.highlightBlock(block);
                });
        }, 20);
    },


    scrollBottom: function scrollBottom() {
        setTimeout(function () {
            document.documentElement.scrollTop = document.documentElement.scrollHeight;
        }, 20);
    }
};

vm.initialize();



console.log("loaded...");

