    /*
    ** Watch_Face_Editor tool
    ** watchface js version v2.0.1
    ** Copyright © CashaCX75. All Rights Reserved
    */

    try {
    (()=>{
        //dynamic modify start

        //Variable declaration section

        //dynamic modify end
        const e = __$$hmAppManager$$__.currentApp;
        const o = e.current
          , {px: i} = (new DeviceRuntimeCore.WidgetFactory(new DeviceRuntimeCore.HmDomApi(e,o)),
        e.__globals__)
          , n = Logger.getLogger("watchface6");
        o.module = DeviceRuntimeCore.WatchFace({
            init_view() {
                //dynamic modify start
                    
                //Item description section

                //dynamic modify end
            },
            onInit() {
                n.log("index page.js on init invoke")
            },
            build() {
                this.init_view(),
                n.log("index page.js on ready invoke")
            },
            onDestroy() {
                n.log("index page.js on destroy invoke")
            }
        })
    }
    )()
} catch (e) {
    console.log("Mini Program Error", e),
    e && e.stack && e.stack.split(/\n/).forEach((e=>console.log("error stack", e)))
}
