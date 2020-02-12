/*! jQuery Mobile v1.2.0 jquerymobile.com | jquery.org/license */

(function (a, b, c) { typeof define == "function" && define.amd ? define(["jquery"], function (d) { return c(d, a, b), d.mobile; }) : c(a.jQuery, a, b); })(this, document, function (a, b, c, d) {
    (function (a, b) { var d = { touch: "ontouchend" in c }; a.mobile = a.mobile || {}, a.mobile.support = a.mobile.support || {}, a.extend(a.support, d), a.extend(a.mobile.support, d); })(a), function (a, b, c, d) {
        function x(a) {
            while (a && typeof a.originalEvent != "undefined")
                a = a.originalEvent; return a;
        } function y(b, c) {
            var e = b.type, f, g, i, k, l, m, n, o, p; b = a.Event(b), b.type = c, f = b.originalEvent, g = a.event.props, e.search(/^(mouse|click)/) > -1 && (g = j); if (f)
                for (n = g.length, k; n;)
                    k = g[--n], b[k] = f[k]; e.search(/mouse(down|up)|click/) > -1 && !b.which && (b.which = 1); if (e.search(/^touch/) !== -1) {
                        i = x(f), e = i.touches, l = i.changedTouches, m = e && e.length ? e[0] : l && l.length ? l[0] : d;
                        if (m)
                            for (o = 0, p = h.length; oe || Math.abs(c.pageY - n) > e, o && !d && H("vmousecancel", b, f), H("vmousemove", b, f), F();)
                                ;
                    } function M(a) {
                        if (r)
                            return; C(); var b = z(a.target), c; H("vmouseup", a, b); if (!o) {
                                var d = H("vclick", a, b);
                                d && d.isDefaultPrevented() && (c = x(a).changedTouches[0], p.push({ touchID: v, x: c.clientX, y: c.clientY }), q = !0);
                            } H("vmouseout", a, b), o = !1, F();
                    } function N(b) {
                        var c = a.data(b, e), d; if (c)
                            for (d in c)
                                if (c[d])
                                    return !0; return !1;
                    } function O() { } function P(b) { var c = b.substr(1); return { setup: function (d, f) { N(this) || a.data(this, e, {}); var g = a.data(this, e); g[b] = !0, k[b] = (k[b] || 0) + 1, k[b] === 1 && t.bind(c, I), a(this).bind(c, O), s && (k.touchstart = (k.touchstart || 0) + 1, k.touchstart === 1 && t.bind("touchstart", J).bind("touchend", M).bind("touchmove", L).bind("scroll", K)); }, teardown: function (d, f) { --k[b], k[b] || t.unbind(c, I), s && (--k.touchstart, k.touchstart || t.unbind("touchstart", J).unbind("touchmove", L).unbind("touchend", M).unbind("scroll", K)); var g = a(this), h = a.data(this, e); h && (h[b] = !1), g.unbind(c, O), N(this) || g.removeData(e); } }; } var e = "virtualMouseBindings", f = "virtualTouchID", g = "vmouseover vmousedown vmousemove vmouseup vclick vmouseout vmousecancel".split(" "), h = "clientX clientY pageX pageY screenX screenY".split(" "), i = a.event.mouseHooks ? a.event.mouseHooks.props : [], j = a.event.props.concat(i), k = {}, l = 0, m = 0, n = 0, o = !1, p = [], q = !1, r = !1, s = "addEventListener" in c, t = a(c), u = 1, v = 0, w; a.vmouse = { moveDistanceThreshold: 10, clickDistanceThreshold: 10, resetTimerDuration: 1500 }; for (var Q = 0; Qa.event.special.swipe.scrollSupressionThreshold && b.preventDefault();)
                ;
        } var e = b.originalEvent.touches ? b.originalEvent.touches[0] : b, f = { time: (new Date).getTime(), coords: [e.pageX, e.pageY], origin: a(b.target) }, g; c.bind(i, j).one(h, function (b) { c.unbind(i, j), f && g && g.time - f.timea.event.special.swipe.horizontalDistanceThreshold && Math.abs(f.coords[1] - g.coords[1]); g.coords[0] ? "swipeleft" : "swiperight"; }), f = g = d;
    };
});
a.each({ scrollstop: "scrollstart", taphold: "tap", swipeleft: "swipe", swiperight: "swipe" }, function (b, c) { a.event.special[b] = { setup: function () { a(this).bind(c, a.noop); } }; });
(a, this);