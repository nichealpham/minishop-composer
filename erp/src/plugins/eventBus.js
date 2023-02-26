import Vue from "vue";

class EventBus {
  constructor() {
    this.bus = new Vue();
  }

  /**
   * Listen for the given event.
   *
   * @param {string} event
   * @param {function} handler
   */
  on(event, handler) {
    this.bus.$on(event, handler);
  }

  /**
   * Listen for the given event once.
   *
   * @param {string} event
   * @param {function} handler
   */
  once(event, handler) {
    this.bus.$once(event, handler);
  }

  /**
   * Remove one or more event listeners.
   *
   * @param {string} event
   * @param {function} handler
   */
  off(event, handler) {
    this.bus.$off(event, handler);
  }

  /**
   * Emit the given event.
   *
   * @param {string|object} event
   * @param {...*} args
   */
  emit(event, ...args) {
    this.bus.$emit(event, ...args);
  }
}

export default {
  install(Vue) {
    const bus = new EventBus();
    Vue.prototype.$bus = bus;
  },
};
