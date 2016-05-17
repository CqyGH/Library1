var CODE = CODE || {};

CODE = function(o_input, n_codeLength, s_color, n_width, n_height) {
	this.random = new Array('0','1','2','3','4','5','6','7','8','9');//随机字符
	this.code = new Array();
	this.codeLength = n_codeLength || 4;
	this.color = s_color;
	this.statue = false;
	this.width = n_width || 120;
	this.height = n_height || 40;

	this.input = o_input || null;

	this.createCanvas();
	this.createDOMElement();

	this.createNew();
}
//,'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
CODE.prototype = {
	checkCode: function() {
		this.upperInput();
		if(!this.doCheck()) {
           alert("请重新输入验证码!");
			this.createNew();
			this.clearInput();
			return false;
		} else {
			return true;
		}
	},

	createNew: function(){
		this.statue = false;
		this.createCode();
		this.showCode();
	},

	createCanvas: function() {
		var canvas = document.createElement("canvas");
		this.context = canvas.getContext("2d");
		canvas.width = this.width;
		canvas.height = this.height;
		return canvas;
	},

	getState: function() {
		return this.statue;
	},

	createDOMElement: function() {
		var _self = this;
		this.domElement = document.createElement("div");
		this.domElement.addEventListener("click", function(){
    		_self.onClick();
		});
		this.domElement.appendChild(this.context.canvas);
	},

	appendTo: function(element) {
		element.appendChild(this.domElement);
		this.domElement.style.width = this.width+ "px";
		this.domElement.style.height = this.height + "px";
	},

	showCode: function(context) {
		this.context.save();
		this.context.fillStyle = "#ffffff";
		this.context.fillRect(0,0,this.width,this.height);
		if(!!this.color) {
			this.context.strokeStyle = this.color;
		} else {
			this.context.strokeStyle = "red";
		}
		this.context.bezierCurveTo(0, this.height, this.width/2, 0, this.width, this.height);
		this.context.stroke();
		this.context.restore();
		for(var i=0;i<this.codeLength;i++) {
			this.showText(i);
		}
	},

	showText: function(i) {
		this.context.save();
		if(!!this.color) {
			this.context.fillStyle = this.color;
		} else {
			this.context.fillStyle = "red";
		}
		this.context.font = "bold " + this.width/4+ "px sans-serif";
		this.context.textAlign= 'center';
		this.context.textBaseline= 'middle';
		this.context.translate(this.width*(i+1/2)*(38/40)/(this.codeLength),this.height/2);
		this.context.translate(Math.floor(Math.random()*10),Math.floor(Math.random()*10));
		this.context.rotate(Math.PI*Math.floor(Math.random()*15)/40-Math.PI/8);
		this.context.fillText(this.code[i],0,0);
		this.context.restore();
	},

	createCode: function() {
		this.code = new Array();
		for(var i = 0; i < this.codeLength; i++) {//循环操作  
        	var index = Math.floor(Math.random()*10);//取得随机数的索引（0~35）  
        	this.code.push(this.random[index]);//根据索引取得随机数加到code上  
    	}
	},
	
 	onClick: function() {
 		this.createNew();
 	},

 	clearInput: function() {
 		this.input.value = "";
 		this.input.style.color = "#000000";
 	},

 	upperInput: function() {
 		var upperValue = this.input.value.toUpperCase();
 		this.input.value = upperValue;
 		this.input.style.color = "#cccccc";
 	},

	doCheck: function() {
		var answer = this.input.value.toUpperCase().split("");
		if(answer.length !== this.codeLength) {
      

			return false;
		} else {
			for(var i=0;i<this.codeLength;i++) {
				if(answer[i] !== this.code[i]) {
              
					return false;
				}
			}
		}
		this.statue = true;
		return true;
	},
}