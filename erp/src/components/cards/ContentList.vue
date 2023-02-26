<template>
  <v-list v-if="template && template.title">
    <v-list-item class="content_card_title">
      <v-list-item-title class="pl-3 pr-3 text-capitalize">
        <b>{{ title }}</b>
      </v-list-item-title>
      <slot name="title-controller" />
    </v-list-item>
    <content-item
      v-for="(item, index) in items"
      :key="item.title + index"
      :item="item"
      @view="$emit('view', item)"
    >
      <template v-slot:controller>
        <slot name="card-controller" :item="item" />
      </template>
    </content-item>
    <h5 v-if="!items.length" class="no_data">
      {{ $t("common.nodata") }}
    </h5>
  </v-list>
</template>

<script>
import ContentItem from "./ContentItem.vue";

export default {
  components: { ContentItem },
  props: {
    template: {
      type: Object,
      default() {
        return {
          title: "",
          items: [],
        };
      },
    },
  },
  watch: {
    template: {
      deep: true,
      immediate: true,
      handler(value) {
        this.render(value);
      },
    },
  },
  data() {
    return {
      title: "",
      items: [],
      noData: false,
      defaultImage: "/icons/service.png",
    };
  },
  methods: {
    getTitleByLanguage(title) {
      if (!title) return "";
      var result = this.$t(`cardTitle.${title.toLowerCase()}`);
      if (!result.includes("cardTitle.")) return result;
      return title;
    },
    async render(template) {
      if (!template) return;
      var { title, items } = template;
      this.title = this.getTitleByLanguage(title);
      this.items = items;
    },
  },
};
</script>
