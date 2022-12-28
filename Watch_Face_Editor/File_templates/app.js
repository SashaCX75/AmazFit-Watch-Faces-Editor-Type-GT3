try {
    (()=>{
        const e = __$$hmAppManager$$__.currentApp;
        e.__globals__ = {
            lang: new DeviceRuntimeCore.HmUtils.Lang(DeviceRuntimeCore.HmUtils.getLanguage()),
            px: DeviceRuntimeCore.HmUtils.getPx(454)
        };
        const {px: t} = e.__globals__;
        function n() {
            if ("undefined" != typeof self)
                return self;
            if ("undefined" != typeof window)
                return window;
            if ("undefined" != typeof global)
                return global;
            if ("undefined" != typeof globalThis)
                return globalThis;
            throw new Error("unable to locate global object")
        }
        e.__globals__.gettext = DeviceRuntimeCore.HmUtils.gettextFactory({}, e.__globals__.lang, "en-US");
        let r = n();
        r.Logger || "undefined" != typeof DeviceRuntimeCore && (r.Logger = DeviceRuntimeCore.HmLogger);
        let o = n();
        o.Buffer || ("undefined" != typeof Buffer ? o.Buffer = Buffer : o.Buffer = DeviceRuntimeCore.Buffer);
        let i = n();
        "undefined" == typeof setTimeout && "undefined" != typeof timer && (i.clearTimeout = function(e) {
            e && timer.stopTimer(e)
        }
        ,
        i.setTimeout = function(e, t) {
            const n = timer.createTimer(t || 1, Number.MAX_SAFE_INTEGER, (function() {
                i.clearTimeout(n),
                e && e()
            }
            ), {});
            return n
        }
        ,
        i.clearImmediate = function(e) {
            e && timer.stopTimer(e)
        }
        ,
        i.setImmediate = function(e) {
            const t = timer.createTimer(1, Number.MAX_SAFE_INTEGER, (function() {
                i.clearImmediate(t),
                e && e()
            }
            ), {});
            return t
        }
        ,
        i.clearInterval = function(e) {
            e && timer.stopTimer(e)
        }
        ,
        i.setInterval = function(e, t) {
            return timer.createTimer(1, t, (function() {
                e && e()
            }
            ), {})
        }
        ),
        e.app = DeviceRuntimeCore.App({
            globalData: {},
            onCreate(e) {},
            onDestroy(e) {},
            onError(e) {},
            onPageNotFound(e) {},
            onUnhandledRejection(e) {}
        })
    }
    )()
} catch (e) {
    console.log("Mini Program Error", e),
    e && e.stack && e.stack.split(/\n/).forEach((e=>console.log("error stack", e)))
}
