#pragma strict	

var particles:GameObject[];			
var maxButtons:int = 10;			
private var page:int = 0;			
private var pages:int;				
private var currentParticle:GameObject;

function Start(){
	
    particles.Sort(particles, function(P1,P2) String.Compare(P1.name, P2.name));
	
	pages = Mathf.Ceil((particles.length -1 )/ maxButtons);

}

function OnGUI () {
	
	Time.timeScale = GUI.VerticalSlider (Rect (185, 50, 20, 150), Time.timeScale, 2.0, 0.0);
	
	if(particles.length > maxButtons){
		
		if(GUI.Button(Rect(20,(maxButtons+1)*18,75,18),"Prev"))if(page > 0)page--;else page=pages;
		
		if(GUI.Button(Rect(95,(maxButtons+1)*18,75,18),"Next"))if(page < pages)page++;else page=0;
		
		GUI.Label (Rect(60,(maxButtons+2)*18,150,22), "Page" + (page+1) + " / " + (pages+1));
		
	}
	
	
	var pageButtonCount:int = particles.length - (page*maxButtons);
	
	if(pageButtonCount > maxButtons)pageButtonCount = maxButtons;
	
	
	for(var i:int=0;i < pageButtonCount;i++){
		if(GUI.Button(Rect(20,i*18+18,150,18),particles[i+(page*maxButtons)].transform.name)){
			if(currentParticle) Destroy(currentParticle);
			var go:GameObject = Instantiate(particles[i+page*maxButtons]);
			currentParticle = go;
			ParticleFX(go.GetComponent(ParticleSystem), i + (page * maxButtons) +1);
		}
	}
}

function ParticleFX (PFX:ParticleSystem, PPFX:int){
		Time.timeScale = 1;
		PFX.Play();
}