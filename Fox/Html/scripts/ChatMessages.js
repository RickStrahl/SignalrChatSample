var app = null;
var vm = {
    fox: null,    
    messages: [],
        //  { message: "first message", name: "Rick", group: "Markdown Monster", isActiveUser: false },    
        //  { message: "second message", name: "Rick", group: "Markdown Monster", isActiveUser: false } ],    
    initialize: function initialize(){        
        // open in new window
        $(document.body).on("click",".message-item a", function() {
            window.open(this.href);
            return false;
        });
    },
    addmessage: function(message, name, isActiveUser) {
        // Create a msg to add to messages array in model 
        // that VueJs can bind to
        var msg = { message: message, name: name, isActiveUser: isActiveUser};

        // fix up message with Markdown
        msg.message = marked(msg.message);
       
        vm.messages.push(msg);        
        if (vm.messages > 30)
            vm.messages.splice(0,10)               

        vm.scrollBottom();
        vm.highlightCode();                        
    },
    addmessagejson: function(jsonMessage){        
        var message = JSON.parse(jsonMessage);

        var msg = { message: message.Message, name: message.User.Name, isActiveUser: message.IsActiveUser};        
        
        // fix up message with Markdown
        vm.message = marked(msg.message);

        vm.messages.push(msg);
        if (vm.messages > 30)
            vm.messages.splice(0,10)      
                            
        vm.scrollBottom();
        vm.highlightCode();
    },
    clearmessages: function(reserved) {
        vm.messages = [];
    },
    highlightCode: function() {
        setTimeout(function() {
            $("pre code")
            .each(function (i, block) {
                hljs.highlightBlock(block);
            });
        },20);
    },


    scrollBottom: function scrollBottom() {
        setTimeout( function() {
            document.documentElement.scrollTop = document.documentElement.scrollHeight; 
        },20);
    }

}
vm.initialize();

// Fox Called method that receive objects - methods have to be on the doc root
// for non-simple parms.
function initializeinterop(fox) {
    vm.fox = fox;    
    console.log("initializeInterop");       
    //vm.fox.sendmessage("JavaScript InitializeInterop","Southwest Fox");
    return vm;
}
// do nothing console
if (!window.console)
{
    window.console = { log: function(message) { vm.fox.setstatus(message.toString()); } }
}

// Initialize Vue js for databinding
app = new Vue({
    el: "#Page",
    data: vm
});

console.log("loaded...");

